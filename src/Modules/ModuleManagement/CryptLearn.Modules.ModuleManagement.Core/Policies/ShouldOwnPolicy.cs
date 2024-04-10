using CryptLearn.Modules.ModuleManagement.Core.Repositories;
using CryptLearn.Shared.Abstractions.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CryptLearn.Modules.ModuleManagement.Core.Policies
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

            httpContextAccessor.HttpContext.Request.EnableBuffering();
            var id = (await httpContextAccessor.HttpContext.Request.ReadFromJsonAsync<WithId>()).Id;
            httpContextAccessor.HttpContext.Request.Body.Position = 0;
            var module = await modulesRepository.GetAsync(id);
            return module is not null && module.AuthorId.ToString() == context.User.Identity.Name;
        }

        private record WithId(Guid Id);
    }
}
