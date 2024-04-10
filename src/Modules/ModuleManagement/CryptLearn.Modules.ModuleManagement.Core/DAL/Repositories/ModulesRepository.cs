using CryptLearn.Modules.ModuleManagement.Core.DAL.DbContexts;
using CryptLearn.Modules.ModuleManagement.Core.Entities;
using CryptLearn.Modules.ModuleManagement.Core.Exceptions;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleManagement.Core.DAL.Repositories
{

    internal class ModulesRepository : IModulesRepository
    {
        private readonly DbSet<Module> _modules;
        private readonly ModuleManagementDbContext _dbContext;

        public ModulesRepository(ModuleManagementDbContext dbContext)
        {
            _modules = dbContext.Modules;
            _dbContext = dbContext;
        }

        public async Task AddAsync(Module module, CancellationToken cancellationToken = default)
        {
            await _modules.AddAsync(module, cancellationToken);
        }

        public async Task AddAsync(Template template, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(template, cancellationToken);
        }

        public void Delete(Module module)
        {
            _modules.Remove(module);
        }

        public IQueryable<Module> GetAll()
        {
            return _modules
                .Include(x => x.Templates);
        }

        public async Task<Module> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var module = await GetAll().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if(module is null)
            {
                throw new ModuleNotFoundException(id);
            }
            return module;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
