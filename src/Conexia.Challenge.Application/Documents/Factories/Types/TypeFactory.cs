using Conexia.Challenge.Application.Documents.Factories.Types.Interfaces;
using Conexia.Challenge.Application.Documents.Strategies.Types.Interfaces;
using Conexia.Challenge.Domain.Documents.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Conexia.Challenge.Application.Documents.Factories.Types
{
    public class TypeFactory : ITypeFactory
    {
        readonly IEnumerable<ITypeStrategy> _strategies;

        public TypeFactory(IEnumerable<ITypeStrategy> strategies)
        {
            _strategies = strategies;
        }

        public ITypeStrategy Create(DocumentType type) =>
            _strategies.SingleOrDefault(x => x.Is(type));
    }
}
