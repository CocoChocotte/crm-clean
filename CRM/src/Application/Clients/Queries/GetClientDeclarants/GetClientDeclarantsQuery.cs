using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CRM.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Clients.Queries.GetClientDeclarants
{
    public class GetClientDeclarantsQuery : IRequest<List<DeclarantDto>>
    {
        public int IdClient { get; set; }
    }
    
    public class GetClientQueryHandler : IRequestHandler<GetClientDeclarantsQuery,List<DeclarantDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClientQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<DeclarantDto>> Handle(GetClientDeclarantsQuery request, CancellationToken cancellationToken)
        {
            var declarants = await _context.Declarants.Where(d => d.IdClient == request.IdClient).ToListAsync(cancellationToken);

            return _mapper.Map<List<DeclarantDto>>(declarants);
        }
    }
}