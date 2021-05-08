using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRM.Application.Common.Interfaces;
using CRM.Application.Taches.Queries.Dtos;
using CRM.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Taches.Queries.GetTaches
{
    public class GetTachesQuery : IRequest<TachesVm>
    {
    }

    public class GetTachesQueryHandler : IRequestHandler<GetTachesQuery, TachesVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetTachesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser, IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
            _identityService = identityService;
        }

        public async Task<TachesVm> Handle(GetTachesQuery request, CancellationToken cancellationToken)
        {
            var taches = await _context.Taches
                // .Include(x => x.UtilisateurNavigation)
                .Include(x => x.Client)
                // .Where(x => ( x.IdUtilisateur == 0) && x.Client.IdResponsable == _currentUser.IdentityId)
                .ProjectTo<TacheDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var responsable = await _identityService.FindUserByIdAsync(_currentUser.IdentityId, cancellationToken);
            
            var tachesVm = new TachesVm
            {
                TachesTerminees = new List<TacheDto>(),
                TachesAFaire = new List<TacheDto>(),
                TachesEnRetard = new List<TacheDto>(),
                TachesEncours = new List<TacheDto>()
            };

            if (taches == null || taches.Count == 0)
            {
                return tachesVm;
            }

            taches = taches.Select(t =>
            {
                t.Responsable = responsable;

                return t;
            }).ToList();

            foreach (var tache in taches)
            {
                if (tache.EnRetard && tache.StatutTache != StatutTache.Terminee)
                {
                    tachesVm.TachesEnRetard.Add(tache);
                }
                else if (tache.StatutTache == StatutTache.Terminee)
                {
                    tachesVm.TachesTerminees.Add(tache);
                }
                else if (tache.StatutTache == StatutTache.AFaire)
                {
                    tachesVm.TachesAFaire.Add(tache);
                }
                else
                {
                    tachesVm.TachesEncours.Add(tache);
                }
            }

            return tachesVm;
        }
    }
}