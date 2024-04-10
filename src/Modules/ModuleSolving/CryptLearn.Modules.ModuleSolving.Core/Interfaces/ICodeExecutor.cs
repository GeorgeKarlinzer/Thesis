namespace CryptLearn.Modules.ModuleSolving.Core.Interfaces;

internal interface ICodeExecutor
{ 
    string Language { get; }
    Task<(string output, string errors)> RunCodeAsync(string code, string tests, CancellationToken cancellationToken);
}
