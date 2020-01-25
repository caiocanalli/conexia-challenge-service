using Conexia.Challenge.Domain.Documents.Enums;

namespace Conexia.Challenge.Application.Documents.Requests
{
    public class UpdateRequest
    {
        public int Id { get; set; }
        public DocumentSituation Situation { get; set; }
    }
}
