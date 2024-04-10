using CryptLearn.Modules.ModuleSolving.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.Configurations
{
    internal class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(ModuleManagement.Shared.Configurations.MaxCodeLength)
                .IsRequired();

            builder.HasOne<Module>()
                .WithMany(x => x.Solutions)
                .HasForeignKey(x => x.ModuleId)
                .IsRequired();

            builder.HasOne(x => x.Language)
                .WithMany()
                .IsRequired();
        }
    }
}
