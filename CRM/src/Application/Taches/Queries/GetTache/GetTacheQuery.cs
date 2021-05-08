using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Application.Common.Exceptions;
using CRM.Application.Common.Interfaces;
using CRM.Application.Taches.Queries.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Taches.Queries.GetTache
{
    public class GetTacheQuery : IRequest<TacheDto>
    {
        public int IdTache { get; set; }
    }

    public class GetTacheQueryHandler : IRequestHandler<GetTacheQuery, TacheDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetTacheQueryHandler(IApplicationDbContext context, IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUserService;
        }

        public async Task<TacheDto> Handle(GetTacheQuery request, CancellationToken cancellationToken)
        {
            var tache = await _context.Taches.Where(t => t.Id == request.IdTache)
                .Include(x => x.Client)
                // .Include(x => x.UtilisateurNavigation)
                .SingleOrDefaultAsync(cancellationToken);

            if (tache == null)
            {
                throw new NotFoundException();
            }

            // if (tache.IdClient.HasValue && tache.Client.IdResponsable != _currentUser.IdentityId)
            // {
            //         throw new UnauthorizedAccessException();
            //     
            // }

            return _mapper.Map<TacheDto>(tache);

        }
    }
}