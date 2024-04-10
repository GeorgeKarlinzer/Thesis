using CryptLearn.Modules.Languages.Core.DTO;
using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.Languages.Core.Queries;
internal record GetLanguages() : IQuery<IEnumerable<LanguageDto>>;
