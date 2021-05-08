using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Clients.Queries.GetClients
{
    public class GetClientsQuery: IRequest<List<ClientDto>>
    {
    }
    
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, List<ClientDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetClientsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<List<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _context.Clients
                .Include(x => x.IdResponsable)
                // .Where(x => x.IdResponsable == _currentUser.IdentityId)
                .ToListAsync(cancellationToken);
            
            return _mapper.Map<List<ClientDto>>(clients);
            
        }
    }
}