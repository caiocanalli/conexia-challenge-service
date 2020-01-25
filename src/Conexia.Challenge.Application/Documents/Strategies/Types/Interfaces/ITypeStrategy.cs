using Conexia.Challenge.Domain.Documents.Enums;
using System.Threading.Tasks;

namespace Conexia.Challenge.Application.Documents.Strategies.Types.Interfaces
{
    public interface ITypeStrategy
    {
        bool Is(DocumentType type);
        Task ProcessAsync();
    }
}
