using AutoMapper;
using Conexia.Challenge.Application.Documents.Factories.Types.Interfaces;
using Conexia.Challenge.Application.Documents.Requests;
using Conexia.Challenge.Application.Documents.Responses;
using Conexia.Challenge.Application.Documents.Services.Interfaces;
using Conexia.Challenge.Application.Infrastructure.Exceptions;
using Conexia.Challenge.Domain;
using Conexia.Challenge.Domain.Documents;
using Conexia.Challenge.Domain.Documents.Enums;
using Conexia.Challenge.Domain.Documents.Interfaces;
using Conexia.Challenge.Infra.Framework.Contracts;
using Conexia.Challenge.Infra.Logging.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        readonly ILoggerAdapter<DocumentAppService> _logger;

        readonly string _documentPath = string.Empty;

        readonly Dictionary<DocumentSituation, Func<int, Task>> _situationsToProcess;
        readonly Dictionary<string, DocumentType> _validDocumentTypes;

        public DocumentAppService(
            IBus bus,
            IMapper mapper,
            IDocumentService documentService,
            IUnitOfWorkFactory unitOfWorkFactory,
            ITypeFactory typeFactory,
            ILoggerAdapter<DocumentAppService> logger,
            IHostingEnvironment hostingEnvironment)
        {
            _bus = bus;
            _mapper = mapper;
            _documentService = documentService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _typeFactory = typeFactory;
            _logger = logger;

            _documentPath = Path.Combine(hostingEnvironment.WebRootPath, "docs");

            _situationsToProcess = new Dictionary<DocumentSituation, Func<int, Task>>
            {
                { DocumentSituation.Approved, ApprovedAsync },
                { DocumentSituation.Disapproved, DisapprovedAsync }
            };

            _validDocumentTypes = new Dictionary<string, DocumentType>
            {
                { "csv", DocumentType.Csv },
                { "xls", DocumentType.Xls },
                { "xlsx", DocumentType.Xls }
            };
        }

        public async Task UploadAsync(UploadRequest request)
        {
            using (var unitOfWork = _unitOfWorkFactory.StartUnitOfWork())
            {
                try
                {
                    await RegisterDocumentAsync(request.Name, request.File);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();

                    _logger.LogError(ex, $"Error on upload Document");

                    throw;
                }
            }
        }

        public async Task<FilterResponse> FilterAsync(FilterRequest request)
        {
            using (_unitOfWorkFactory.StartUnitOfWork())
            {
                var result = await _documentService.FilterAsync(
                    request.Page,
                    request.PageSize,
                    request.Name,
                    DocumentType.Csv,
                    DocumentStatus.ToProcess,
                    DocumentSituation.Disapproved);

                FilterResponse response =
                    _mapper.Map<FilterResponse>(result);

                return response;
            }
        }

        public async Task UpdateAsync(UpdateRequest request)
        {
            if (_situationsToProcess.TryGetValue(request.Situation, out var SituationToProcess))
            {
                await SituationToProcess(request.Id);
            }

            throw new ApplicationLayerException();
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
                    return;
                }

                await strategy.ProcessAsync();

                unitOfWork.Commit();
            }
        }

        async Task RegisterDocumentAsync(string name, IFormFile file)
        {
            var document = new Document
            {
                Name = name,
                Type = DocumentType.Csv//GetDocumentType(file.FileName)
            };

            await _documentService.AddAsync(document);
            //await SaveDocumentAsync(document.Id, file);
        }

        async Task SaveDocumentAsync(int id, IFormFile file)
        {
            var filePath = $"{_documentPath}{id}";

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
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

                    await _bus.Publish<EvaluateDocumentEvent>(new
                    {
                        Id = id
                    });
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();

                    _logger.LogError(ex, $"Error on Approved Document");

                    throw;
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
                    unitOfWork.Rollback();

                    _logger.LogError(ex, $"Error on Disapproved Document");

                    throw;
                }
            }
        }

        DocumentType GetDocumentType(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            if (_validDocumentTypes.TryGetValue(extension, out var documentType))
            {
                return documentType;
            }

            throw new ApplicationLayerException();
        }
    }
}
