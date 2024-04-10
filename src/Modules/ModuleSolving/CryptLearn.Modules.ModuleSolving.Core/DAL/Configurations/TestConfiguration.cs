using CryptLearn.Modules.ModuleSolving.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.Configurations
{
    internal class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Module>()
                .WithMany(x => x.Tests)
                .HasForeignKey(x => x.ModuleId)
                .IsRequired();

            builder.HasOne(x => x.Language)
                .WithMany()
                .IsRequired();

            builder.Property(x => x.Code)
                .HasMaxLength(ModuleManagement.Shared.Configurations.MaxCodeLength)
                .IsRequired();
        }
    }
}
