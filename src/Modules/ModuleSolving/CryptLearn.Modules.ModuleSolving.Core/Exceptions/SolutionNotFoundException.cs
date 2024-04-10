using CryptLearn.Shared.Abstractions.Exceptions;

namespace CryptLearn.Modules.ModuleSolving.Core.Exceptions
{
    internal class SolutionNotFoundException : CryptLearnException
    {
        public SolutionNotFoundException() : base($"Solution was not found!")
        {
        }
    }
}
