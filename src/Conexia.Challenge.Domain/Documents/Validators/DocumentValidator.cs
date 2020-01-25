using FluentValidation;

namespace Conexia.Challenge.Domain.Documents.Validators
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            SetForName();
            SetForType();
            SetForStatus();
            SetForSituation();
            SetForCreatedDate();
        }

        void SetForName()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("O nome do documento é obrigatório")
                .NotEmpty()
                .WithMessage("O nome do documento não pode ser vazio")
                .MinimumLength(5)
                .WithMessage("O nome do documento deve ter no mínimo 5 caracteres")
                .MaximumLength(100)
                .WithMessage("O nome do documento pode ter no máximo 100 caracteres");
        }

        void SetForType()
        {
            RuleFor(x => x.Type)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("O tipo do documento é obrigatório")
                .NotEmpty()
                .WithMessage("O tipo do documento não pode ser vazio");
        }

        void SetForStatus()
        {
            RuleFor(x => x.Status)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("O status do documento é obrigatório")
                .NotEmpty()
                .WithMessage("O status do documento não pode ser vazio");
        }

        void SetForSituation()
        {
            RuleFor(x => x.Situation)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("A situação do documento é obrigatória")
                .NotEmpty()
                .WithMessage("A situação do documento não pode ser vazia");
        }

        void SetForCreatedDate()
        {
            RuleFor(x => x.Created)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .WithMessage("A data de criação do documento é obrigatória")
                .NotEmpty()
                .WithMessage("A data de criação do documento não pode ser vazia");
        }
    }
}