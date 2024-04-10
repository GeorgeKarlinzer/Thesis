using CryptLearn.Modules.ModuleManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptLearn.Modules.ModuleManagement.Core.DAL.Configurations
{
    internal class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Shared.Configurations.MaxModuleNameLength)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(Shared.Configurations.MaxModuleDescriptionLength)
                .IsRequired();

            builder.Property(x => x.AuthorName)
                .IsRequired();
        }
    }
}
