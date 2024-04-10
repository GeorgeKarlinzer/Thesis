using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CryptLearn.Modules.ModuleSolving.Core.Services
{
    internal class CodeTester : ICodeTester
    {
        private readonly string _codeExecutorEndpoint;

        public CodeTester(IConfiguration configuration)
        {
            _codeExecutorEndpoint = configuration.GetValue<string>("Modules:ModuleSolving:CodeExecutorEndpoint");
        }

        public async Task<SolutionResultDto> TestAsync(string language, string solution, string tests, CancellationToken cancellationToken = default)
        {
            var client = new HttpClient();
            var response = await client.PostAsJsonAsync(_codeExecutorEndpoint, new {code = solution, tests, language}, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<SolutionResultDto>(cancellationToken: cancellationToken);
            return result;
        }
    }
}
