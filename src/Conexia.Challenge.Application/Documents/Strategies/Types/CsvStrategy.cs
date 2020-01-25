using Conexia.Challenge.Domain.Documents.Enums;
using System.Threading.Tasks;

namespace Conexia.Challenge.Application.Documents.Strategies.Types
{
    public class CsvStrategy : TypeStrategy
    {
        public override bool Is(DocumentType type) =>
            type == DocumentType.Csv;

        public override async Task ProcessAsync()
        {
        }
    }
}
