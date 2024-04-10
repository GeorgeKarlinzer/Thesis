using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleSolving.Core.Queries;

internal record GetSolvedModules(Guid UserId)  : IQuery<IEnumerable<Guid>>;