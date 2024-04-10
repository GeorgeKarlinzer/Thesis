using CryptLearn.Modules.ModuleManagement.Core.Entities;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;

namespace CryptLearn.Modules.ModuleManagement.Unit.Moqs
{
    internal class InMemoryModulesRepository : IModulesRepository
    {
        public List<Module> Modules { get; } = new();

        public Task AddAsync(Module module)
        {
            Modules.Add(module);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Module>> BrowseAsync()
        {
            return Task.FromResult(Modules.AsEnumerable());
        }

        public Task DeleteAsync(Module module)
        {
            Modules.Remove(module);
            return Task.CompletedTask;
        }

        public Task<Module> GetAsync(Guid id)
        {
            return Task.FromResult(Modules.SingleOrDefault(x => x.Id == id));
        }

        public Task UpdateAsync(Module module)
        {
            return Task.CompletedTask;
        }
    }
}
