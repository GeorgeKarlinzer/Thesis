using CryptLearn.Modules.ModuleManagement.Core.DAL.DbContexts;
using CryptLearn.Modules.ModuleManagement.Core.Entities;
using CryptLearn.Modules.ModuleManagement.Core.Exceptions;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleManagement.Core.DAL.Repositories
{
    internal class LanguagesRepository : ILanguagesRepository
    {
        private readonly ModuleManagementDbContext _dbContext;

        public LanguagesRepository(ModuleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Language language, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(language, cancellationToken);
        }

        public void Delete(Language language)
        {
            _dbContext.Remove(language);
        }

        public IQueryable<Language> GetAll()
        {
            return _dbContext.Languages;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
