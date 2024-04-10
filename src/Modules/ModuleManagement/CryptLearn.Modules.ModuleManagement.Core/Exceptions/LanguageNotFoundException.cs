using CryptLearn.Shared.Abstractions.Exceptions;

namespace CryptLearn.Modules.ModuleManagement.Core.Exceptions
{
    internal class LanguageNotFoundException : CryptLearnException
    {
        public string Name { get; }

        public LanguageNotFoundException(string name) : base($"Language with name '{name}' was not found!")
        {
            Name = name;
        }
    }
}
