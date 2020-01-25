using Conexia.Challenge.Domain;
using Conexia.Challenge.Domain.Documents;
using Conexia.Challenge.Domain.Documents.Interfaces;
using Conexia.Challenge.Domain.Models;
using Conexia.Challenge.Infra.Data.Extensions;
using NHibernate.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Conexia.Challenge.Infra.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        readonly UnitOfWork _unitOfWork;

        public DocumentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        public async Task AddAsync(Document document) =>
            await _unitOfWork.Current.SaveAsync(document);

        public async Task<Document> GetAsync(int id) =>
            await _unitOfWork.Current.Query<Document>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(Document document) =>
            await _unitOfWork.Current.UpdateAsync(document);

        public async Task<PagedResult<Document>> FilterAsync(int page, int pageSize, string name) =>
            await _unitOfWork.Current
                .Query<Document>()
                .Where(x => x.Name == name)
                .RecoverPagedAsync(page, pageSize);
    }
}
