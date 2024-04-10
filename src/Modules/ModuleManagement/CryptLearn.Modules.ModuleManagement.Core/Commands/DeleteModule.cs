using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleManagement.Core.Commands;

internal record DeleteModule(Guid Id) : ICommand;
