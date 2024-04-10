using CryptLearn.Modules.ModuleManagement.Core.Entities;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;
using CryptLearn.Modules.ModuleManagement.Shared.Notifications;
using CryptLearn.Shared.Abstractions.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CryptLearn.Modules.ModuleManagement.Core.Commands;

internal class ModuleManagementCommandsHandler : ICommandHandler<CreateModule>, ICommandHandler<UpdateModule>, ICommandHandler<DeleteModule>
{
    private readonly IModulesRepository _modulesRepository;
    private readonly ILanguagesRepository _languagesRepository;
    private readonly IPublisher _publisher;
    private readonly IHttpContextAccessor _httpContext;

    public ModuleManagementCommandsHandler(IModulesRepository modulesRepository, ILanguagesRepository languagesRepository, IPublisher publisher, IHttpContextAccessor httpContext)
    {
        _modulesRepository = modulesRepository;
        _languagesRepository = languagesRepository;
        _publisher = publisher;
        _httpContext = httpContext;
    }

    public async Task Handle(DeleteModule request, CancellationToken cancellationToken)
    {
        var module = await _modulesRepository.GetAsync(request.Id, cancellationToken);
        _modulesRepository.Delete(module);
        await _publisher.Publish(new ModuleDeleted(module.Id), cancellationToken);
        await _modulesRepository.SaveAsync(cancellationToken);
    }

    public async Task Handle(UpdateModule request, CancellationToken cancellationToken)
    {
        var module = await _modulesRepository.GetAsync(request.Id, cancellationToken);

        module.Description = request.Description;
        module.Templates.Clear();

        foreach (var code in request.Codes)
        {
            await _modulesRepository.AddAsync(new Template()
            {
                Id = Guid.NewGuid(),
                Code = code.Template,
                LanguageName = code.Language,
                Module = module
            }, cancellationToken);
        }

        await _publisher.Publish(new ModuleUpdated(module.Id, request.Codes.Select(x => (x.Language, x.Solution, x.Tests))), cancellationToken);
        await _modulesRepository.SaveAsync(cancellationToken);
    }

    public async Task Handle(CreateModule request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_httpContext.HttpContext.User.Identity.Name);
        var userName = _httpContext.HttpContext.User.Claims.First(x => x.Type == "userName").Value;
        var moduleId = Guid.NewGuid();

        var module = new Module
        {
            Id = moduleId,
            Name = request.Name,
            Description = request.Description,
            AuthorName = userName,
            AuthorId = userId,
            Templates = new(),
        };
        module.Templates.AddRange(request.Codes.Select(x => new Template()
        {
            Id = Guid.NewGuid(),
            Code = x.Template,
            LanguageName = x.Language,
            Module = module
        }));
        await _modulesRepository.AddAsync(module, cancellationToken);
        await _publisher.Publish(new ModuleCreated(module.Id, module.AuthorId, request.Codes.Select(x => (x.Language, x.Solution, x.Tests))), cancellationToken);
        await _modulesRepository.SaveAsync(cancellationToken);
    }
}
