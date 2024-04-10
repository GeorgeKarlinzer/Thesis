using CryptLearn.Modules.ModuleSolving.Core.Commands;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleSolving.Core.Validators;
internal class TestSolutionValidator : AbstractValidator<TestSolution>
{
	public TestSolutionValidator(IModulesRepository modulesRepository)
	{
		RuleFor(x => x.ModuleId)
			.MustAsync(async (moduleId, ctk) => await modulesRepository.GetAll().AnyAsync(x => x.Id == moduleId, ctk))
			.WithMessage("Module does not exists");

		RuleFor(x => x.Language)
			.MustAsync(async (lang, ctk) => await modulesRepository.GetAll().SelectMany(x => x.Tests).AnyAsync(x => x.Language.Name.ToLower() == lang.ToLower() && x.Language.IsActive, ctk))
			.WithMessage("There are no tests for requested language");

		RuleFor(x => x.Code)
			.MaximumLength(ModuleManagement.Shared.Configurations.MaxCodeLength);
	}
}
