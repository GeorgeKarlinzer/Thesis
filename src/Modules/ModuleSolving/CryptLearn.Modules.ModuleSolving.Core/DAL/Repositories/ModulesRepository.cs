using CryptLearn.Modules.ModuleSolving.Core.DAL.DbContexts;
using CryptLearn.Modules.ModuleSolving.Core.Entities;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.Repositories
{
    internal class ModulesRepository : IModulesRepository
    {
        private readonly ModuleSolvingDbContext _dbContext;

        public ModulesRepository(ModuleSolvingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Module module, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(module, cancellationToken);
        }

        public void Delete(Module module)
        {
            _dbContext.Remove(module);
        }

        public IQueryable<Module> GetAll()
        {
            return _dbContext.Modules;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
