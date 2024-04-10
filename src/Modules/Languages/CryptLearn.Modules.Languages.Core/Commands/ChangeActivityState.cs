using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.Languages.Core.Commands;
internal record ChangeActivityState(string Name) : ICommand;
