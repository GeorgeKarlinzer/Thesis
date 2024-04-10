using CryptLearn.Modules.ModuleSolving.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.DbContexts
{
    internal class ModuleSolvingDbContext : DbContext
    {
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Test> Tests { get; set; }

        public ModuleSolvingDbContext(DbContextOptions<ModuleSolvingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
