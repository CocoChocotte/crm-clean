using CRM.Application.Clients.Queries.GetClient;
using FluentValidation;

namespace CRM.Application.Clients.Queries.GetClientDeclarants
{
    public class GetClientDeclarantQueryValidator : AbstractValidator<GetClientQuery>
    {
        public GetClientDeclarantQueryValidator()
        {
            RuleFor(f => f.IdClient)
                .NotEmpty().WithMessage("L'id client est requis");
        }
    }
}