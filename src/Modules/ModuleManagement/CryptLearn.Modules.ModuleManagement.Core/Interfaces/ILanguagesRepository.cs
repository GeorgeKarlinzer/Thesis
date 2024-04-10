using CryptLearn.Modules.ModuleManagement.Core.Entities;

namespace CryptLearn.Modules.ModuleManagement.Core.Repositories
{
    internal interface ILanguagesRepository
    {
        IQueryable<Language> GetAll();
        Task AddAsync(Language language, CancellationToken cancellationToken = default);
        void Delete(Language language);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
