using AutoMapper;
using CryptLearn.Modules.ModuleManagement.Core.DTOs;
using CryptLearn.Modules.ModuleManagement.Core.Entities;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;
using CryptLearn.Shared.Abstractions.Cqrs;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleManagement.Core.Queries;

internal class ModuleManagementQueriesHandler : IQueryHandler<GetModules, IEnumerable<ModuleListItemDto>>, IQueryHandler<GetModule, ModuleDto>, IQueryHandler<GetUserModules, IEnumerable<Guid>>
{
    private readonly IModulesRepository _repository;
    private readonly IMapper _mapper;

    public ModuleManagementQueriesHandler(IModulesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<IEnumerable<ModuleListItemDto>> Handle(GetModules request, CancellationToken cancellationToken)
    {
        var modules = _repository.GetAll();
        var dtos = _mapper.Map<IEnumerable<Module>, IEnumerable<ModuleListItemDto>>(modules);
        return Task.FromResult(dtos);
    }

    public async Task<ModuleDto> Handle(GetModule request, CancellationToken cancellationToken)
    {
        var module = await _repository.GetAsync(request.Id, cancellationToken);
        var dto = _mapper.Map<Module, ModuleDto>(module);
        return dto;
    }

    public async Task<IEnumerable<Guid>> Handle(GetUserModules request, CancellationToken cancellationToken)
    {
        var modulesId = await _repository.GetAll().Where(x => x.AuthorId == request.UserId).Select(x => x.Id).ToListAsync(cancellationToken);
        return modulesId;
    }
}