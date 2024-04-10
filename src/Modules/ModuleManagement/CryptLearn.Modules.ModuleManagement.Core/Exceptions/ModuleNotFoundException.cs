using CryptLearn.Shared.Abstractions.Exceptions;

namespace CryptLearn.Modules.ModuleManagement.Core.Exceptions
{
    internal class ModuleNotFoundException : CryptLearnException
    {
        public Guid Id { get; }
        public ModuleNotFoundException(Guid id) : base($"Module with id: '{id}' was not found!")
        {
            Id = id;
        }
    }
}
