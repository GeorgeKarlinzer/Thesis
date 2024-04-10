using CryptLearn.Modules.AccessControl.Core;
using CryptLearn.Modules.Languages.Core;
using CryptLearn.Modules.ModuleManagement.Core;
using CryptLearn.Modules.ModuleSolving.Core;
using CryptLearn.Shared.Abstractions.Modules;
using CryptLearn.Shared.Infrastructure;

var modules = new List<IModule>
{
    new AccessControlModule(),
    new ModuleManagementModule(),
    new ModuleSolvingModule(),
    new LanguagesModule()
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration, modules);
foreach (var module in modules)
{
    module.Add(builder.Services, builder.Configuration);
}

var app = builder.Build();

app.UseInfrastructure();
foreach (var module in modules)
{
    await module.Use(app, builder.Configuration);
}

app.MapControllers();

app.Run();
