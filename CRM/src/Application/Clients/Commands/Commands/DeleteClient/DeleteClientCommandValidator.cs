using FluentValidation;

namespace CRM.Application.Clients.Commands.Commands.DeleteClient
{
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator()
        {
            RuleFor(f => f.Id)
                .NotEmpty().WithMessage("L'id de la tache est requis");
        }
    }
}