using System;
using System.Threading;
using System.Threading.Tasks;
using CRM.Application.Common.Exceptions;
using CRM.Application.Common.Interfaces;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Taches.Commands.TermineeTache
{
    public class TermineeTacheCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class TermineeTacheCommandHanlder : IRequestHandler<TermineeTacheCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public TermineeTacheCommandHanlder(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(TermineeTacheCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Taches.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Tache), request.Id);
            }

            // if (entity.IdUtilisateur != _currentUser.IdentityId)
            // {
            //     throw new UnauthorizedAccessException();
            // }

            entity.StatutTache = StatutTache.Terminee;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}