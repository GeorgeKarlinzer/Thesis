using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.Languages.Core.Commands;

internal record CreateLanguage(string Name) : ICommand;