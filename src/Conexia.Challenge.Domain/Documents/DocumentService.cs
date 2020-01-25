using Conexia.Challenge.Domain.Core.Exceptions;
using Conexia.Challenge.Domain.Documents.Interfaces;
using Conexia.Challenge.Domain.Documents.Validators;
using Conexia.Challenge.Domain.Models;
using System.Threading.Tasks;

namespace Conexia.Challenge.Domain.Documents
{
    public class DocumentService : IDocumentService
    {
        readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task AddAsync(Document document)
        {
            var result = await new DocumentValidator().ValidateAsync(document);

            if (!result.IsValid)
            {
                throw new DomainException(result.ToString());
            }

            await _documentRepository.AddAsync(document);
        }

        public async Task<Document> GetAsync(int id) =>
            await _documentRepository.GetAsync(id);

        public async Task UpdateAsync(Document document)
        {
            var result = await new DocumentValidator().ValidateAsync(document);

            if (!result.IsValid)
            {
                throw new DomainException(result.ToString());
            }

            await _documentRepository.UpdateAsync(document);
        }

        public async Task<PagedResult<Document>> FilterAsync(int page, int pageSize, string name) =>
            await _documentRepository.FilterAsync(page, pageSize, name);
    }
}
