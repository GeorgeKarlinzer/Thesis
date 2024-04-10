using CryptLearn.Modules.ModuleSolving.Core.Entities;

namespace CryptLearn.Modules.ModuleSolving.Core.Interfaces
{
    internal interface IModulesRepository
    {
        IQueryable<Module> GetAll();
        Task AddAsync(Module module, CancellationToken cancellationToken = default);
        void Delete(Module module);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
