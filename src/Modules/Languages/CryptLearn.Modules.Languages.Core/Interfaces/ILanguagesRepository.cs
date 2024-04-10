using CryptLearn.Modules.Languages.Core.Models;

namespace CryptLearn.Modules.Languages.Core.Interfaces;
internal interface ILanguagesRepository
{
    IQueryable<Language> GetAll();
    Task AddAsync(Language language, CancellationToken cancellationToken = default);
    Task SaveAsync(CancellationToken cancellationToken = default);
}
