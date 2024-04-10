using CryptLearn.Modules.Languages.Core.Commands;
using CryptLearn.Modules.Languages.Core.Interfaces;
using CryptLearn.Modules.Languages.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.Languages.Core.Validators;
internal class CreateLanguageValidator : AbstractValidator<CreateLanguage>
{
	public CreateLanguageValidator(ILanguagesRepository repository)
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.MaximumLength(Configurations.MaxNameLength)
			.MustAsync(async (name, cancellationToken) =>
			{
				var isExist = await repository.GetAll().AnyAsync(x => x.Name == name.ToUpperInvariant(), cancellationToken);
				return !isExist;
			}).WithMessage(x => $"Language with a name '{x.Name}' already exists");
	}
}