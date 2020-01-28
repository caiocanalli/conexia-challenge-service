using Conexia.Challenge.Domain.Documents.Enums;
using Conexia.Challenge.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Conexia.Challenge.Domain.Documents.Interfaces
{
    public interface IDocumentRepository
    {
        Task AddAsync(Document document);
        Task<Document> GetAsync(int id);
        Task UpdateAsync(Document document);
        Task<PagedResult<Document>> FilterAsync(
            int page,
            int pageSize,
            string name,
            DocumentType type,
            DocumentStatus status,
            DocumentSituation situation);
    }
}
