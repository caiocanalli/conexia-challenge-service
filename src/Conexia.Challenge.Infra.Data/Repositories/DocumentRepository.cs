using Conexia.Challenge.Domain;
using Conexia.Challenge.Domain.Documents.Interfaces;

namespace Conexia.Challenge.Infra.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        readonly UnitOfWork _unitOfWork;

        public DocumentRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }
    }
}
