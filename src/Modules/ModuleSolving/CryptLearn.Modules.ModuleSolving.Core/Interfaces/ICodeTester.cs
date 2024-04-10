using CryptLearn.Modules.ModuleSolving.Core.DTOs;

namespace CryptLearn.Modules.ModuleSolving.Core.Interfaces
{
    internal interface ICodeTester
    {
        Task<SolutionResultDto> TestAsync(string language, string solution, string tests, CancellationToken cancellationToken = default);
    }
}
