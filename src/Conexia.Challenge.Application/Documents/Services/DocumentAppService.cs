using AutoMapper;
using Conexia.Challenge.Application.Documents.Factories.Types.Interfaces;
using Conexia.Challenge.Application.Documents.Requests;
using Conexia.Challenge.Application.Documents.Responses;
using Conexia.Challenge.Application.Documents.Services.Interfaces;
using Conexia.Challenge.Application.Exceptions;
using Conexia.Challenge.Domain;
using Conexia.Challenge.Domain.Documents;
using Conexia.Challenge.Domain.Documents.Enums;
using Conexia.Challenge.Domain.Documents.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conexia.Challenge.Application.Documents.Services
{
    public class DocumentAppService : IDocumentAppService
    {
        readonly IBus _bus;
        readonly IMapper _mapper;
        readonly IDocumentService _documentService;
        readonly IUnitOfWorkFactory _unitOfWorkFactory;
        readonly ITypeFactory _typeFactory;

        readonly Dictionary<DocumentSituation, Func<int, Task>> _situationsToProcess;

        public DocumentAppService(
            IBus bus,
            IMapper mapper,
            IDocumentService documentService,
            IUnitOfWorkFactory unitOfWorkFactory,
            ITypeFactory typeFactory)
        {
            _bus = bus;
            _mapper = mapper;
            _documentService = documentService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _typeFactory = typeFactory;

            _situationsToProcess = new Dictionary<DocumentSituation, Func<int, Task>>
            {
                { DocumentSituation.Approved, ApprovedAsync },
                { DocumentSituation.Disapproved, DisapprovedAsync }
            };
        }

        public async Task UploadAsync(UploadRequest request)
        {
            // Realizar upload do arquivo para S3

            using (var unitOfWork = _unitOfWorkFactory.StartUnitOfWork())
            {
                try
                {
                    var document = new Document
                    {
                        Name = request.Name,
                        Type = DocumentType.Csv // Get Type from file
                    };

                    await _documentService.AddAsync(document);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // Log information
                    unitOfWork.Rollback();
                }
            }
        }

        public async Task<FilterResponse> FilterAsync(FilterRequest request)
        {
            using (_unitOfWorkFactory.StartUnitOfWork())
            {
                var result = await _documentService.FilterAsync(
                    request.Page, request.PageSize, request.Name);

                FilterResponse response =
                    _mapper.Map<FilterResponse>(result);

                return response;
            }
        }

        public async Task UpdateAsync(UpdateRequest request)
        {
            if (!_situationsToProcess.TryGetValue(request.Situation, out var SituationToProcess))
            {
                throw new ApplicationLayerException();
            }

            await SituationToProcess(request.Id);
        }

        public async Task EvaluateAsync(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.StartUnitOfWork())
            {
                Document document = await _documentService.GetAsync(id);

                if (document == null)
                {
                    throw new ApplicationLayerException();
                }

                var strategy = _typeFactory.Create(document.Type);

                if (strategy == null)
                {
                    // Log information
                    return;
                }

                await strategy.ProcessAsync();

                unitOfWork.Commit();
            }
        }

        async Task ApprovedAsync(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.StartUnitOfWork())
            {
                try
                {
                    Document document = await _documentService.GetAsync(id);

                    if (document == null)
                    {
                        throw new ApplicationLayerException();
                    }

                    document.ChangeDocumentStatusToProcessing();
                    document.ChangeDocumentSituationToApproved();
                    await _documentService.UpdateAsync(document);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // Log information
                    unitOfWork.Rollback();
                }
            }
        }

        async Task DisapprovedAsync(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.StartUnitOfWork())
            {
                try
                {
                    Document document = await _documentService.GetAsync(id);

                    if (document == null)
                    {
                        throw new ApplicationLayerException();
                    }

                    document.ChangeDocumentStatusToSuccessfullyProcessed();
                    document.ChangeDocumentSituationToDisapproved();
                    await _documentService.UpdateAsync(document);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    // Log Information
                    unitOfWork.Rollback();
                }
            }
        }
    }
}
