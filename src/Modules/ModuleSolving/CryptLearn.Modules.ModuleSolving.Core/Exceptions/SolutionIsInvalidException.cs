using CryptLearn.Shared.Abstractions.Exceptions;

namespace CryptLearn.Modules.ModuleSolving.Core.Exceptions
{
    internal class SolutionIsInvalidException : CryptLearnException
    {
        public SolutionIsInvalidException(string language) : base($"Submitted solution for language '{language}' is invalid!")
        {
        }
    }
}
