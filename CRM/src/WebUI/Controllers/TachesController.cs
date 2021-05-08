using System.Threading.Tasks;
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
    public class TachesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<TachesVm>> GetTaches([FromQuery] GetTachesQuery query)
        {
            query ??= new GetTachesQuery();
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TacheDto>> GetTache([FromRoute] int id)
        {
            return await Mediator.Send(new GetTacheQuery { IdTache = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> AddTache(CreateTacheCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTache([FromRoute] int id, UpdateTacheCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/terminee")]
        public async Task<ActionResult> TermineeTache([FromRoute] int id)
        {
            await Mediator.Send(new TermineeTacheCommand { Id = id });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteTacheCommand() { Id = id });

            return NoContent();
        }

    }
}