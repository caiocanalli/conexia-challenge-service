using Conexia.Challenge.Domain.Documents.Enums;
using System.Threading.Tasks;

namespace Conexia.Challenge.Application.Documents.Strategies
{
    public class XlsStrategy : TypeStrategy
    {
        public override bool Is(DocumentType type) =>
            type == DocumentType.Xls;

        public override async Task ProcessAsync()
        {
        }
    }
}
