using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CRM.Application.Common.Exceptions;
using CRM.Application.Common.Interfaces;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Clients.Commands.Commands.CreateClient

{
    public class CreateClientCommand : IRequest
    {
        public string Nom { get; set; }
        
        public string Prenom { get; set; }
        
        public DateTime DateDeNaissance { get; set; }
        
        public string Commentaire { get; set; }

        public TypeClient Type { get; set; }
        
        public CategorieClient Categorie { get; set; }
        
        public List<Email> Emails { get; set; }
        
        public Civilite Civilite { get; set; }
        
        public List<Lieu> Adresses { get; set; }
        
        public List<Telephone> Telephones { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IIdentityService _identityService;

        public CreateClientCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IIdentityService identityService)
        {
            _context = context;
            _currentUser = currentUser;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var responsable = await _identityService.FindUserByIdAsync(_currentUser.IdentityId, cancellationToken);
            
            if (responsable == null)
            {
                throw new NotFoundException(nameof(responsable));
            }
            
            var client = new Client
            {
                Nom = request.Nom,
                Prenom = request.Prenom,
                Civilite = request.Civilite,
                DateDeNaissance = request.DateDeNaissance,
                Commentaire = request.Commentaire,
                // IdResponsable = _currentUser.IdentityId,
                // Responsable = responsable,
                Type = request.Type,
                Categorie = request.Categorie,
                Emails = request.Emails,
                Adresses = request.Adresses,
                Telephones = request.Telephones
            };

            await _context.Clients.AddAsync(client, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}