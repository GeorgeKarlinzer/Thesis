using CryptLearn.Modules.ModuleSolving.Core.DAL.DbContexts;
using CryptLearn.Modules.ModuleSolving.Core.Entities;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.Repositories
{
    internal class LanguageRepository : ILanguageRepository
    {
        private readonly ModuleSolvingDbContext _dbContext;

        public LanguageRepository(ModuleSolvingDbContext dbContext)
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
            return _dbContext.Set<Language>();
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
