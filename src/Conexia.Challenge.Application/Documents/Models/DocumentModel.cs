using Conexia.Challenge.Domain.Documents.Enums;
using System;

namespace Conexia.Challenge.Application.Documents.Models
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DocumentType Type { get; set; }
        public DocumentStatus Status { get; set; }
        public DocumentSituation Situation { get; set; }
        public DateTime Created { get; set; }
    }
}
