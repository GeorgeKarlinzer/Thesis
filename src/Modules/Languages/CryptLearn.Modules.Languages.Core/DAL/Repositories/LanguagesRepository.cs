using CryptLearn.Modules.Languages.Core.DAL.DbContexts;
using CryptLearn.Modules.Languages.Core.Interfaces;
using CryptLearn.Modules.Languages.Core.Models;

namespace CryptLearn.Modules.Languages.Core.DAL.Repositories;
internal class LanguagesRepository : ILanguagesRepository
{
    private readonly ModuleDbContext _dbContext;

    public LanguagesRepository(ModuleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Language language, CancellationToken cancellationToken = default)
    {
        await _dbContext.Languages.AddAsync(language, cancellationToken);
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
