using CryptLearn.Modules.ModuleSolving.Core.Entities;

namespace CryptLearn.Modules.ModuleSolving.Core.Interfaces
{
    internal interface ISolutionsRepository
    {
        IQueryable<Solution> GetAll();
        Task AddAsync(Solution solution, CancellationToken cancellationToken = default);
        void Delete(Solution solution);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
