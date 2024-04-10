using CryptLearn.Modules.ModuleSolving.Core.DAL.Repositories;
using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Modules.ModuleSolving.Core.Entities;
using CryptLearn.Modules.ModuleSolving.Core.Exceptions;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using CryptLearn.Shared.Abstractions.Cqrs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace CryptLearn.Modules.ModuleSolving.Core.Commands;

internal class CommandsHandler : ICommandHandler<TestSolution, SolutionResultDto>
{
    private readonly ISolutionsRepository _repository;
    private readonly IModulesRepository _modulesRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly ICodeTester _codeTester;
    private readonly IHttpContextAccessor _contextAccessor;

    public CommandsHandler(ISolutionsRepository repository, IModulesRepository modulesRepository, ILanguageRepository languageRepository, ICodeTester codeTester, IHttpContextAccessor contextAccessor)
    {
        _repository = repository;
        _modulesRepository = modulesRepository;
        _languageRepository = languageRepository;
        _codeTester = codeTester;
        _contextAccessor = contextAccessor;
    }

    public async Task<SolutionResultDto> Handle(TestSolution request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_contextAccessor.HttpContext.User.Identity.Name);

        var test = (await _modulesRepository
            .GetAll()
            .Include(x => x.Tests)
            .ThenInclude(x => x.Language)
            .FirstAsync(x => request.ModuleId == request.ModuleId, cancellationToken))
            .Tests
            .FirstOrDefault(x => x.Language.Name.ToLower() == request.Language.ToLower() && x.Language.IsActive)
            ?? throw new LanguageIsInactiveException(request.Language);

        var result = await _codeTester.TestAsync(request.Language, request.Code, test.Code, cancellationToken);
        if (result.Success && request.ShouldBeSubmitted)
        {
            var existingSolution = await _repository
                .GetAll()
                .FirstOrDefaultAsync(x => x.ModuleId == request.ModuleId && x.UserId == userId && x.Language.Name.ToLower() == request.Language.ToLower() && x.Language.IsActive, cancellationToken);
            if(existingSolution is not null)
            {
                _repository.Delete(existingSolution);
            }

            var solution = new Solution()
            {
                ModuleId = request.ModuleId,
                Code = request.Code,
                Language = await _languageRepository.GetAll().FirstAsync(x => x.Name.ToLower() == request.Language.ToLower() && x.IsActive, cancellationToken),
                UserId = userId
            };
            await _repository.AddAsync(solution, cancellationToken);
            await _repository.SaveAsync(cancellationToken);
        }
        return result;
    }
}