using CryptLearn.Shared.Abstractions.Exceptions;

namespace CryptLearn.Modules.Languages.Core.Exceptions;
internal class LanguageNotFoundException : CryptLearnException
{
    public string Name { get; }

    public LanguageNotFoundException(string name) : base($"Programming language with a name '{name}' was not found")
    {
        Name = name;
    }
}
