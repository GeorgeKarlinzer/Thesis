using CryptLearn.Modules.ModuleManagement.Core.Entities;

namespace CryptLearn.Modules.ModuleManagement.Core.Repositories
{
    internal interface IModulesRepository
    {
        IQueryable<Module> GetAll();
        Task<Module> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Module module, CancellationToken cancellationToken = default);
        Task AddAsync(Template template, CancellationToken cancellationToken = default);
        void Delete(Module module);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
