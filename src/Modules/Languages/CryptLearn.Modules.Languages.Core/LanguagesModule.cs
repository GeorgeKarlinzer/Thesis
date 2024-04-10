using CryptLearn.Modules.Languages.Core.DAL.DbContexts;
using CryptLearn.Modules.Languages.Core.DAL.Repositories;
using CryptLearn.Modules.Languages.Core.Interfaces;
using CryptLearn.Modules.Languages.Core.Mappers;
using CryptLearn.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptLearn.Modules.Languages.Core;
public class LanguagesModule : IModule
{
    public string Name => "Languages";

    public void Add(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Modules:Languages:ConnectionString");

        services.AddScoped<ILanguagesRepository, LanguagesRepository>();
        services.AddDbContext<ModuleDbContext>(x => x.UseSqlServer(connectionString));
    }

    public Task Use(IApplicationBuilder app, IConfiguration configuration, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
