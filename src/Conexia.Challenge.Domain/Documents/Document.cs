using Conexia.Challenge.Domain.Core;
using Conexia.Challenge.Domain.Documents.Enums;
using System;

namespace Conexia.Challenge.Domain.Documents
{
    public class Document : IAggregateRoot
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DocumentType Type { get; set; }
        public virtual DocumentStatus Status { get; protected set; }
        public virtual DocumentSituation Situation { get; protected set; }
        public virtual DateTime Created { get; protected set; }

        public Document()
        {
            Status = DocumentStatus.ToProcess;
            Situation = DocumentSituation.Disapproved;
            Created = DateTime.UtcNow;
        }

        public virtual void ChangeDocumentStatusToProcessing() =>
            Status = DocumentStatus.Processing;

        public virtual void ChangeDocumentStatusToSuccessfullyProcessed() =>
            Status = DocumentStatus.SuccessfullyProcessed;

        public virtual void ChangeDocumentSituationToApproved() =>
            Situation = DocumentSituation.Approved;

        public virtual void ChangeDocumentSituationToDisapproved() =>
            Situation = DocumentSituation.Disapproved;
    }
}
