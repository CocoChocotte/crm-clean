using System;
using FluentValidation;

namespace CRM.Application.Taches.Commands.UpdateTache
{
    public class UpdateTacheCommandValidator : AbstractValidator<UpdateTacheCommand>
    {
        public UpdateTacheCommandValidator()
        {
            RuleFor(f => f.Id)
                .NotEmpty().WithMessage("L'id de la tache est requis");

            RuleFor(t => t.Nom)
                .MaximumLength(200)
                .NotEmpty();
            
            RuleFor(t => t.Echeance)
                .Must(BeAValidDate).WithMessage("Echeance est requis")
                .NotEmpty();
        }

        private bool BeAValidDate(DateTime? date)
        {
            return !date.Equals(default) && date.HasValue && date.Value.Year > 1900;
        }
    }
}