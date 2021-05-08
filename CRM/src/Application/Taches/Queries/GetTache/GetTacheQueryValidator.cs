using CRM.Application.Taches.Queries.GetTache;
using FluentValidation;

namespace CapitalExplorer.V2.Application.Taches.Queries.GetTache
{
    public class GetTacheQueryValidator : AbstractValidator<GetTacheQuery>
    {
        public GetTacheQueryValidator()
        {
            RuleFor(f => f.IdTache)
                .NotEmpty().WithMessage("L'id de la tache est requis");
        }
    }
}