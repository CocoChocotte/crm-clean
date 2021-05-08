using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Application.Common.Exceptions;
using CRM.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Clients.Queries.GetClient
{
    public class GetClientQuery : IRequest<ClientDetailDto>
    {
        public int IdClient { get; set; }
    }

    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, ClientDetailDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetClientQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ClientDetailDto> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.Where(t => t.Id == request.IdClient)
                .SingleOrDefaultAsync(cancellationToken);

            if (client == null)
            {
                throw new NotFoundException();
            }

            if (client.IdResponsable != _currentUser.UserId)
            {
                throw new UnauthorizedAccessException();
            }

            return _mapper.Map<ClientDetailDto>(client);
        }
    }
}