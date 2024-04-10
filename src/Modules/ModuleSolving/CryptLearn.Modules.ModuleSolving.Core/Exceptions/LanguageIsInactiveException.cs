using CryptLearn.Shared.Abstractions.Exceptions;

namespace CryptLearn.Modules.ModuleSolving.Core.Exceptions
{
    internal class LanguageIsInactiveException : CryptLearnException
    {
        public LanguageIsInactiveException(string lang) : base($"Programming language {lang} is inactive.")
        {
        }
    }
}
