using CryptLearn.Modules.ModuleManagement.Shared.Notifications;
using CryptLearn.Modules.ModuleSolving.Core.Entities;
using CryptLearn.Modules.ModuleSolving.Core.Exceptions;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace CryptLearn.Modules.ModuleSolving.Core.Notifications;
internal class ModuleNotificationsHandler : INotificationHandler<ModuleCreated>, INotificationHandler<ModuleDeleted>, INotificationHandler<ModuleUpdated>
{
    private readonly IModulesRepository _modulesRepository;
    private readonly ICodeTester _codeTester;
    private readonly ILanguageRepository _languageRepository;

    public ModuleNotificationsHandler(IModulesRepository modulesRepository, ICodeTester codeTester, ILanguageRepository languageRepository)
    {
        _modulesRepository = modulesRepository;
        _codeTester = codeTester;
        _languageRepository = languageRepository;
    }

    public async Task Handle(ModuleUpdated notification, CancellationToken cancellationToken)
    {
        var langs = await _languageRepository.GetAll().ToListAsync(cancellationToken);

        var existingModule = await _modulesRepository.GetAll()
            .Include(x => x.Tests)
            .Include(x => x.Solutions)
            .FirstAsync(x => x.Id == notification.Id, cancellationToken);
        existingModule.Tests.Clear();
        existingModule.Tests.AddRange(notification.Data.Select(x => new Test(existingModule.Id, langs.First(y => y.Name.ToLower() == x.Language.ToLower()), x.Test)));

        var newSolutions = new List<Solution>(notification.Data.Select(x => new Solution(existingModule.Id, existingModule.AthorId, langs.First(y => y.Name.ToLower() == x.Language.ToLower()), x.Solution)));

        foreach (var solution in newSolutions)
        {
            var tests = existingModule.Tests.First(x => x.Language == solution.Language);
            var result = await _codeTester.TestAsync(solution.Language.Name, solution.Code, tests.Code, cancellationToken);
            if (!result.Success)
            {
                throw new SolutionIsInvalidException(solution.Language.Name);
            }
        }
        var solutionsToDelete = existingModule.Solutions.Where(x => x.UserId == existingModule.AthorId).ToList();
        foreach (var s in solutionsToDelete)
        {
            existingModule.Solutions.Remove(s);
        }

        existingModule.Solutions.AddRange(newSolutions);
        await _modulesRepository.SaveAsync(cancellationToken);
    }

    public async Task Handle(ModuleDeleted notification, CancellationToken cancellationToken)
    {
        var existingModule = await _modulesRepository.GetAll().FirstAsync(x => x.Id == notification.Id, cancellationToken);
        _modulesRepository.Delete(existingModule);
        await _modulesRepository.SaveAsync(cancellationToken);
    }

    public async Task Handle(ModuleCreated notification, CancellationToken cancellationToken)
    {
        var langs = await _languageRepository.GetAll().ToListAsync(cancellationToken);
        var tests = notification.Data.Select(x => new Test(notification.Id, langs.First(y => y.Name.ToLower() == x.Language.ToLower()), x.Test)).ToList();
        var solutions = new List<Solution>(notification.Data.Select(x => new Solution(notification.Id, notification.AuthorId, langs.First(y => y.Name.ToLower() == x.Language.ToLower()), x.Solution)));

        foreach (var solution in solutions)
        {
            var test = tests.First(x => x.Language.Name.ToLower() == solution.Language.Name.ToLower());
            var result = await _codeTester.TestAsync(solution.Language.Name.ToUpper(), solution.Code, test.Code, cancellationToken);
            if (!result.Success)
            {
                throw new SolutionIsInvalidException(solution.Language.Name);
            }
        }

        var newModule = new Module()
        {
            Id = notification.Id,
            AthorId = notification.AuthorId,
            Tests = tests,
            Solutions = solutions
        };
        await _modulesRepository.AddAsync(newModule, cancellationToken);
        await _modulesRepository.SaveAsync(cancellationToken);
    }
}
