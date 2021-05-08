using System;
using FluentValidation;

namespace CRM.Application.Clients.Commands.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {

        }

    }
}