using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Interactions;
using Agenda.Domain.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers.Admin
{
    [Route("api/admin/interacoes")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class InteractionController : ControllerBase
    {

        private readonly IInteractionService _interactionService;

        public InteractionController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        [HttpGet("tipos")]
        public ActionResult<IEnumerable<EnumerationResponse>> GetAllInteractionTypes()
        {
            return Ok(_interactionService.GetAllInteractionTypes());
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<Pagination<IEnumerable<InteractionResponse>>>> Search([FromQuery] InteractionSearchParams searchQuery)
        {
            return Ok(await _interactionService.Search(searchQuery));
        }

    }
}
