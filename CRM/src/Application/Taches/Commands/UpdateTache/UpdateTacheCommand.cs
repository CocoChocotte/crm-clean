using System;
using System.Threading;
using System.Threading.Tasks;
using CRM.Application.Common.Exceptions;
using CRM.Application.Common.Interfaces;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Taches.Commands.UpdateTache
{
    public partial class UpdateTacheCommand : IRequest
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int? IdClient { get; set; }

        public DateTime? Echeance { get; set; }

        public StatutTache StatutTache { get; set; }

        public decimal? GainPotentiel { get; set; }
    }

    public class UpdateTacheCommandHandler : IRequestHandler<UpdateTacheCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IIdentityService _identityService;


        public UpdateTacheCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IIdentityService identityService)
        {
            _context = context;
            _currentUser = currentUser;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(UpdateTacheCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Taches.FindAsync(request.Id);
            Client client = null;

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tache), request.Id);
            }

            if (request.IdClient != null)
            {
                client = await _context.Clients
                    .Include(x => x.Responsable)
                    .FirstOrDefaultAsync(x => x.Id == request.IdClient, cancellationToken);

                if (client == null)
                {
                    throw new NotFoundException(nameof(client));
                }

                // if (client.IdResponsable != _currentUser.IdentityId)
                // {
                //     throw new UnauthorizedAccessException();
                // }
            }

            var responsable = await _identityService.FindUserByIdAsync(_currentUser.IdentityId, cancellationToken);


            entity.Nom = request.Nom;
            entity.Description = request.Description;
            entity.IdClient = request.IdClient;
            entity.Client = request.IdClient.HasValue ? client : null;
            // entity.IdUtilisateur = _currentUser.IdentityId;
            // entity.UtilisateurNavigation = responsable;
            entity.Echeance = request.Echeance;
            entity.StatutTache = request.StatutTache;
            entity.GainPotentiel = request.GainPotentiel;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}