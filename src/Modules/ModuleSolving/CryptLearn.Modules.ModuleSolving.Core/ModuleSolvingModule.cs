using CryptLearn.Modules.ModuleSolving.Core.DAL.DbContexts;
using CryptLearn.Modules.ModuleSolving.Core.DAL.Repositories;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using CryptLearn.Modules.ModuleSolving.Core.Services;
using CryptLearn.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CryptLearn.Modules.ModuleSolving.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace CryptLearn.Modules.ModuleSolving.Core
{
    public class ModuleSolvingModule : IModule
    {
        public const string BaseName = "ModuleSolving";
        public string Name => BaseName;

        public void Add(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Modules:ModuleSolving:ConnectionString");

            services.AddDbContext<ModuleSolvingDbContext>(x => x.UseSqlServer(connectionString))
                    .AddScoped<ISolutionsRepository, SolutionsRepository>()
                    .AddScoped<IModulesRepository, ModulesRepository>()
                    .AddScoped<ILanguageRepository, LanguageRepository>()
                    .AddScoped<ICodeTester, CodeTester>();
        }

        public Task Use(IApplicationBuilder app, IConfiguration configuration, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
