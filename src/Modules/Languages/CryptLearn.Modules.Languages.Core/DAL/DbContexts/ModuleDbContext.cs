using CryptLearn.Modules.Languages.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.Languages.Core.DAL.DbContexts;
internal class ModuleDbContext : DbContext
{
    public DbSet<Language> Languages { get; set; }

	public ModuleDbContext(DbContextOptions<ModuleDbContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
