using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.ModuleSolving.Core.Commands;
internal record TestSolution(Guid ModuleId, string Language, string Code, bool ShouldBeSubmitted) : ICommand<SolutionResultDto>;
