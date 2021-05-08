using System;
using System.Threading;
using System.Threading.Tasks;
using CRM.Application.Common.Exceptions;
using CRM.Application.Common.Interfaces;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Taches.Commands.CreateTache

{
    public class CreateTacheCommand : IRequest
    {
        public string Nom { get; set; }
        
        public string Description { get; set; }
        
        public int? IdClient { get; set; }
        
        public DateTime? Echeance { get; set; }
        
        public StatutTache StatutTache { get; set; }
        
        public decimal? GainPotentiel { get; set; }
    }

    public class CreateTacheCommandHandler : IRequestHandler<CreateTacheCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IIdentityService _identityService;


        public CreateTacheCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IIdentityService identityService)
        {
            _context = context;
            _currentUser = currentUser;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(CreateTacheCommand request, CancellationToken cancellationToken)
        {
            var client = new Client();

            if (request.IdClient.HasValue)
            {
                client = await _context.Clients
                    .Include(x => x.Responsable)
                    .FirstOrDefaultAsync(x => x.Id == request.IdClient.Value, cancellationToken);

                if (client?.Responsable == null)
                {
                    throw new NotFoundException(nameof(client));
                }

                // if (client.IdResponsable != _currentUser.IdentityId)
                // {
                //     throw new UnauthorizedAccessException();
                // }
            }
            
            var responsable = await _identityService.FindUserByIdAsync(_currentUser.IdentityId, cancellationToken);

            if (responsable == null)
            {
                throw new NotFoundException(nameof(responsable));
            }
            

            var tache = new Tache
            {
                Nom = request.Nom,
                Description = request.Description,
                IdClient = request.IdClient,
                Client = request.IdClient.HasValue ? client : null,
                // IdUtilisateur = _currentUser.IdentityId,
                // UtilisateurNavigation = responsable,
                Echeance = request.Echeance,
                StatutTache = request.StatutTache,
                GainPotentiel = request.GainPotentiel,
                
            };

            await _context.Taches.AddAsync(tache, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}