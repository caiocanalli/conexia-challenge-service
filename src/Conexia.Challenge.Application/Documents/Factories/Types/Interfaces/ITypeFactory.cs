using Conexia.Challenge.Application.Documents.Strategies.Types.Interfaces;
using Conexia.Challenge.Domain.Documents.Enums;

namespace Conexia.Challenge.Application.Documents.Factories.Types.Interfaces
{
    public interface ITypeFactory
    {
        ITypeStrategy Create(DocumentType type);
    }
}
