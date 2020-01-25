using Conexia.Challenge.Application.Documents.Strategies.Types.Interfaces;
using Conexia.Challenge.Domain.Documents.Enums;
using System.Threading.Tasks;

namespace Conexia.Challenge.Application.Documents.Strategies
{
    public abstract class TypeStrategy : ITypeStrategy
    {
        public abstract bool Is(DocumentType type);
        public abstract Task ProcessAsync();
    }
}
