using CryptLearn.Modules.ModuleSolving.Core.DAL.DbContexts;
using CryptLearn.Modules.ModuleSolving.Core.Entities;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.Repositories
{
    internal class SolutionsRepository : ISolutionsRepository
    {
        private readonly DbSet<Solution> _solutions;
        private readonly ModuleSolvingDbContext _dbContext;

        public SolutionsRepository(ModuleSolvingDbContext dbContext)
        {
            _solutions = dbContext.Solutions;
            _dbContext = dbContext;
        }
        
        public async Task AddAsync(Solution solution, CancellationToken cancellationToken = default)
        {
            await _solutions.AddAsync(solution, cancellationToken);
        }

        public void Delete(Solution solution)
        {
            _solutions.Remove(solution);
        }

        public IQueryable<Solution> GetAll()
        {
            return _solutions.AsQueryable();
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
