using FluentValidation;

namespace CRM.Application.Clients.Queries.GetClient
{
    public class GetClientQueryValidator : AbstractValidator<GetClientQuery>
    {
        public GetClientQueryValidator()
        {
            RuleFor(f => f.IdClient)
                .NotEmpty().WithMessage("L'id client est requis");
        }
    }
}