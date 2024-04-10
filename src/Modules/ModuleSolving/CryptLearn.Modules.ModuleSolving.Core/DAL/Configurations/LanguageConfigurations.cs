using CryptLearn.Modules.ModuleSolving.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.Configurations
{
    internal class LanguageConfigurations : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(x => x.Name);

            builder.Property(x => x.Name)
                .HasMaxLength(Languages.Shared.Configurations.MaxNameLength);

            builder.Property(x => x.IsActive)
                .IsRequired();
        }
    }
}
