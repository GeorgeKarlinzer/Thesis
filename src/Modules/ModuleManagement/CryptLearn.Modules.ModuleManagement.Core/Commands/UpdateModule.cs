using CryptLearn.Modules.ModuleManagement.Core.DTOs;
using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleManagement.Core.Commands;

internal record UpdateModule(Guid Id, string Description, IEnumerable<LanguageCodeDto> Codes) : ICommand;
