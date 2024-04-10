using AutoMapper;
using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Modules.ModuleSolving.Core.Exceptions;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using CryptLearn.Shared.Abstractions.Cqrs;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleSolving.Core.Queries;

internal class QueryHandler : IQueryHandler<GetUserSolution, IEnumerable<SolutionDto>>, IQueryHandler<GetModuleSolutions, IEnumerable<SolutionDto>>, IQueryHandler<GetSolvedModules, IEnumerable<Guid>>, IQueryHandler<GetModuleInfo, IEnumerable<ModuleInfoDto>>
{
    private readonly ISolutionsRepository _repository;
    private readonly IModulesRepository _modulesRepository;
    private readonly IMapper _mapper;

    public QueryHandler(ISolutionsRepository repository, IModulesRepository modulesRepository, IMapper mapper)
    {
        _repository = repository;
        _modulesRepository = modulesRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SolutionDto>> Handle(GetUserSolution request, CancellationToken cancellationToken)
    {
        var solutions = await _repository
            .GetAll()
            .Include(x => x.Language)
            .Where(x => x.ModuleId == request.ModuleId && x.UserId == request.UserId && x.Language.IsActive)
            .ToListAsync(cancellationToken);

        return solutions.Select(x => new SolutionDto(x.Language.Name, x.Code));
    }

    public async Task<IEnumerable<SolutionDto>> Handle(GetModuleSolutions request, CancellationToken cancellationToken)
    {
        var solutions = await _repository
            .GetAll()
            .Include(x => x.Language)
            .Where(x => x.ModuleId == request.ModuleId && x.Language.Name.ToLower() == request.Language.ToLower() && x.Language.IsActive)
            .ToListAsync(cancellationToken);
        return _mapper.Map<IEnumerable<SolutionDto>>(solutions);
    }

    public async Task<IEnumerable<Guid>> Handle(GetSolvedModules request, CancellationToken cancellationToken)
    {
        var modules = await _repository
            .GetAll()
            .Include (x => x.Language)
            .Where(x => x.UserId == request.UserId && x.Language.IsActive)
            .Select(x => x.ModuleId)
            .Distinct()
            .ToListAsync(cancellationToken);

        return modules;
    }

    public async Task<IEnumerable<ModuleInfoDto>> Handle(GetModuleInfo request, CancellationToken cancellationToken)
    {
        var module = await _modulesRepository
            .GetAll()
            .Include(x => x.Tests)
            .ThenInclude(x => x.Language)
            .Include(x => x.Solutions)
            .ThenInclude(x => x.Language)
            .FirstAsync(x => x.Id == request.ModuleId, cancellationToken);
        
        var moduleInfos = module.Tests
            .Select(x => 
            new ModuleInfoDto(x.Language.Name, 
                              module.Solutions
                                .First(s => s.UserId == request.UserId && s.Language.Name.ToLower() == x.Language.Name.ToLower()).Code,
                              x.Code));
        return moduleInfos;
    }
}