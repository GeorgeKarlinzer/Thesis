using CryptLearn.Modules.ModuleManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleManagement.Core.DAL.DbContexts
{

    internal class ModuleManagementDbContext : DbContext
    {
        public DbSet<Module> Modules { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Template> Templates { get; set; }

        public ModuleManagementDbContext(DbContextOptions<ModuleManagementDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
