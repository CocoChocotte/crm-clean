using System;
using FluentValidation;

namespace CRM.Application.Taches.Commands.CreateTache
{
    public class CreateTacheCommandValidator : AbstractValidator<CreateTacheCommand>
    {
        public CreateTacheCommandValidator()
        {
            RuleFor(t => t.Nom)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(t => t.Echeance)
                .Must(BeAValidDate).WithMessage("Echeance is required")
                .NotEmpty();

        }

        private bool BeAValidDate(DateTime? date)
        {
            return !date.Equals(default) && date.HasValue && date.Value.Year > 1900;
        }
    }
}