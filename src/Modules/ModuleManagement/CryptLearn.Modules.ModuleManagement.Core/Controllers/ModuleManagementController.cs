using CryptLearn.Modules.ModuleManagement.Core.Commands;
using CryptLearn.Modules.ModuleManagement.Core.DTOs;
using CryptLearn.Modules.ModuleManagement.Core.Policies;
using CryptLearn.Modules.ModuleManagement.Core.Queries;
using CryptLearn.Shared.Abstractions.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptLearn.Modules.ModuleManagement.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AuthorizeByClaim(PermissionClaims.ReadAccess)]
    internal class ModuleManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ModuleManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleListItemDto>>> GetModules(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetModules(), cancellationToken));
        }

        [HttpGet]
        public async Task<ActionResult<ModuleDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetModule(id), cancellationToken));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<Guid>>> GetUserModules(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetUserModules(Guid.Parse(User.Identity.Name)), cancellationToken));
        }

        [HttpPost]
        [AuthorizeByClaim(PermissionClaims.CreateAccess)]
        public async Task<ActionResult> CreateModule([FromBody] CreateModule command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost]
        [AuthorizeByClaim(PermissionClaims.UpdateAccess), AuthorizeByPolicy<ShouldOwnPolicy>]
        public async Task<ActionResult> UpdateModule([FromBody] UpdateModule command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [AuthorizeByClaim(PermissionClaims.DeleteAccess), AuthorizeByPolicy<ShouldOwnPolicy>]
        public async Task<ActionResult> DeleteModule([FromBody] DeleteModule command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
