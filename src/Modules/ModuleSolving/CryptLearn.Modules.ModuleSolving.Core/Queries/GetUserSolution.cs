using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleSolving.Core.Queries;
internal record GetUserSolution(Guid UserId, Guid ModuleId) : IQuery<IEnumerable<SolutionDto>>;
