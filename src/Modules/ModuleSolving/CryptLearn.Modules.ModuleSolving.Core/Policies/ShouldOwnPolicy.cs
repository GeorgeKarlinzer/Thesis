using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using CryptLearn.Shared.Abstractions.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CryptLearn.Modules.ModuleSolving.Core.Policies
{
    internal class ShouldOwnPolicy : IPolicy
    {
        private readonly IServiceProvider _serviceProvider;

        public IEnumerable<IAuthorizationRequirement> Requirements => new IAuthorizationRequirement[]
        {
            new DenyAnonymousAuthorizationRequirement(),
            new AssertionRequirement(CheckModuleOwnership)
        };

        public ShouldOwnPolicy(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private async Task<bool> CheckModuleOwnership(AuthorizationHandlerContext context)
        {
            using var scope = _serviceProvider.CreateScope();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            var modulesRepository = scope.ServiceProvider.GetRequiredService<IModulesRepository>();

            var id = Guid.Parse(httpContextAccessor.HttpContext.Request.Query["moduleId"]);
            var module = await modulesRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            return module is not null && module.AthorId.ToString() == context.User.Identity.Name;
        }

        private record WithId(Guid ModuleId);
    }
}
