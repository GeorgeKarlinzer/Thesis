using CryptLearn.Modules.ModuleManagement.Core.Commands;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;
using CryptLearn.Modules.ModuleManagement.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleManagement.Core.Validators;
internal class CreateModuleValidator : AbstractValidator<CreateModule>
{
	public CreateModuleValidator(ILanguagesRepository languagesRepository)
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MaximumLength(Configurations.MaxModuleNameLength);

		RuleFor(x => x.Description)
			.NotEmpty()
			.MaximumLength(Configurations.MaxModuleDescriptionLength);

		RuleFor(x => x.Codes)
			.NotEmpty()
			.MustAsync(async (command, ctk) =>
			{
				foreach (var langName in command.Select(x => x.Language))
				{
					var lang = await languagesRepository.GetAll().SingleOrDefaultAsync(x => x.Name == langName && x.IsActive, ctk);
					if (lang is null)
					{
						return false;
					}
				}

				return true;
			})
			.WithMessage("Some of the provided programming languages are not valid")
			.Must(codes => codes.GroupBy(x => x.Language).All(x => x.Count() == 1))
			.WithMessage("Cannot apply several templates for the same language");
	}
}
