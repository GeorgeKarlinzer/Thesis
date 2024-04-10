using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleSolving.Core.Queries;

internal record GetModuleInfo(Guid ModuleId, Guid UserId) : IQuery<IEnumerable<ModuleInfoDto>>;