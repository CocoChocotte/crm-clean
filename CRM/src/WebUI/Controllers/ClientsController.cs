using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Application.Clients.Commands.Commands.CreateClient;
using CRM.Application.Clients.Commands.Commands.DeleteClient;
using CRM.Application.Clients.Commands.Commands.UpdateClient;
using CRM.Application.Clients.Queries.GetClient;
using CRM.Application.Clients.Queries.GetClientDeclarants;
using CRM.Application.Clients.Queries.GetClients;
using CRM.Application.Common.Models;
using CRM.Application.Taches.Commands.CreateTache;
using CRM.Application.Taches.Commands.DeleteTache;
using CRM.Application.Taches.Commands.TermineeTache;
using CRM.Application.Taches.Commands.UpdateTache;
using CRM.Application.Taches.Queries.Dtos;
using CRM.Application.Taches.Queries.GetTache;
using CRM.Application.Taches.Queries.GetTaches;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebUI.Controllers
{
    [Authorize(Policy = Constants.UserPolicies.ConsultantPolicy)]
    public class ClientController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ClientDto>>> GetClients([FromQuery] GetClientsQuery query)
        {
            query ??= new GetClientsQuery();
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDetailDto>> GetClient([FromRoute] int id)
        {
            return await Mediator.Send(new GetClientQuery() { IdClient = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> AddClient(CreateClientCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClient([FromRoute] int id, UpdateClientCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteClientCommand() { Id = id });

            return NoContent();
        }
        
        [HttpGet]
        public async Task<ActionResult<List<DeclarantDto>>> GetClientDeclarant([FromQuery] GetClientDeclarantsQuery query)
        {
            query ??= new GetClientDeclarantsQuery();
            return await Mediator.Send(query);
        }
    }
}