using CryptLearn.Modules.ModuleSolving.Core.Entities;

namespace CryptLearn.Modules.ModuleSolving.Core.Interfaces
{
    internal interface ILanguageRepository
    {
        IQueryable<Language> GetAll();
        Task AddAsync(Language language, CancellationToken cancellationToken = default);
        void Delete(Language language);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
