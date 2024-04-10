using CryptLearn.Modules.ModuleManagement.Core.DAL.DbContexts;
using CryptLearn.Modules.ModuleManagement.Core.DAL.Repositories;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;
using CryptLearn.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CryptLearn.Modules.ModuleManagement.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace CryptLearn.Modules.ModuleManagement.Core
{
    public class ModuleManagementModule : IModule
    {
        public string Name => "Module Management";

        public void Add(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Modules:ModuleManagement:ConnectionString");

            services.AddDbContext<ModuleManagementDbContext>(x => x.UseSqlServer(connectionString))
                    .AddScoped<IModulesRepository, ModulesRepository>()
                    .AddScoped<ILanguagesRepository, LanguagesRepository>();
        }

        public Task Use(IApplicationBuilder app, IConfiguration configuration, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
