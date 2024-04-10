using CryptLearn.Modules.Languages.Core.Commands;
using CryptLearn.Modules.Languages.Core.DTO;
using CryptLearn.Modules.Languages.Core.Queries;
using CryptLearn.Shared.Abstractions.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptLearn.Modules.Languages.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    internal class LanguagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LanguagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Get(int id, string name)
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var a = (JObject)JsonConvert.DeserializeObject(body);
            id = a[nameof(id)].ToObject<int>();
            name = a[nameof(name)].ToObject<string>();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetLanguages()));
        }

        [HttpPost]
        [AuthorizeByClaim(PermissionClaims.UpdateAccess)]
        public async Task<ActionResult> ChangeActivityState([FromBody] ChangeActivityState command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [AuthorizeByClaim(PermissionClaims.CreateAccess)]
        public async Task<ActionResult> Create([FromBody] CreateLanguage command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
