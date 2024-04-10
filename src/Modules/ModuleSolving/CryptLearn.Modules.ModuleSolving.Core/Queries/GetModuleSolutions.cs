using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleSolving.Core.Queries;

internal record GetModuleSolutions(Guid ModuleId, string Language) : IQuery<IEnumerable<SolutionDto>>;
