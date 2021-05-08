using FluentValidation;

namespace CRM.Application.Taches.Commands.DeleteTache
{
    public class DeleteTacheCommandValidator : AbstractValidator<DeleteTacheCommand>
    {
        public DeleteTacheCommandValidator()
        {
            RuleFor(f => f.Id)
                .NotEmpty().WithMessage("L'id de la tache est requis");
        }
    }
}