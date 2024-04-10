using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleManagement.Core.Queries;

internal record GetUserModules(Guid UserId) : IQuery<IEnumerable<Guid>>;