using System;
using System.Threading;
using System.Threading.Tasks;
using CRM.Application.Common.Exceptions;
using CRM.Application.Common.Interfaces;
using CRM.Domain.Entities;
using MediatR;

namespace CRM.Application.Clients.Commands.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IIdentityService _identityService;

        public DeleteClientCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser, IIdentityService identityService)
        {
            _context = context;
            _currentUser = currentUser;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var tache = await _context.Taches.FindAsync(request.Id);

            if (tache == null)
            {
                throw new NotFoundException(nameof(Tache), request.Id);
            }

            // if (tache.IdUtilisateur != _currentUser.IdentityId)
            // {
            //     throw new UnauthorizedAccessException();
            // }

            _context.Taches.Remove(tache);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}