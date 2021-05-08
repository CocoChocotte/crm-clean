using System;
using System.Threading;
using System.Threading.Tasks;
using CRM.Application.Common.Exceptions;
using CRM.Application.Common.Interfaces;
using CRM.Domain.Entities;
using MediatR;

namespace CRM.Application.Taches.Commands.DeleteTache
{
    public class DeleteTacheCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTacheCommandHandler : IRequestHandler<DeleteTacheCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public DeleteTacheCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(DeleteTacheCommand request, CancellationToken cancellationToken)
        {
            var tache = await _context.Taches.FindAsync(request.Id);

            if (tache == null)
            {
                throw new NotFoundException(nameof(Tache), request.Id);
            }

            if (tache.IdUtilisateur != _currentUser.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            _context.Taches.Remove(tache);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}