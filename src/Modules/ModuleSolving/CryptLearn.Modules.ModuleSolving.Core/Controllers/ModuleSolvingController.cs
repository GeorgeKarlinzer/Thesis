using CryptLearn.Modules.ModuleSolving.Core.Commands;
using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Modules.ModuleSolving.Core.Queries;
using CryptLearn.Shared.Abstractions.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CryptLearn.Modules.ModuleSolving.Core.Policies;

namespace CryptLearn.Modules.ModuleSolving.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AuthorizeByClaim(PermissionClaims.Access)]
    internal class ModuleSolvingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ModuleSolvingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<SolutionResultDto>> TestSolution([FromBody] TestSolution command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SolutionDto>>> GetUserSolution(Guid moduleId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetUserSolution(Guid.Parse(User.Identity.Name), moduleId), cancellationToken));
        }

        [HttpGet]
        [AuthorizeByPolicy<ShouldOwnPolicy>]
        public async Task<ActionResult<IEnumerable<SolutionDto>>> GetModuleSolutions(Guid moduleId, string language, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetModuleSolutions(moduleId, language), cancellationToken));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guid>>> GetSolvedModules(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetSolvedModules(Guid.Parse(User.Identity.Name)), cancellationToken));
        }

        [HttpGet]
        [AuthorizeByPolicy<ShouldOwnPolicy>]
        public async Task<ActionResult<IEnumerable<ModuleInfoDto>>> GetModuleInfos(Guid moduleId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetModuleInfo(moduleId, Guid.Parse(User.Identity.Name)), cancellationToken));
        }
    }
}
