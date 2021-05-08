using FluentValidation;

namespace CRM.Application.Taches.Commands.TermineeTache
{
    public class TerminerTacheCommandValidator : AbstractValidator<TermineeTacheCommand>
    {
        public TerminerTacheCommandValidator()
        {
            RuleFor(f => f.Id)
                .NotEmpty().WithMessage("L'id de la tache est requis");
        }
    }
}