using CryptLearn.Modules.ModuleManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptLearn.Modules.ModuleManagement.Core.DAL.Configurations
{
    internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(Languages.Shared.Configurations.MaxNameLength);

            builder.HasKey(x => x.Name);
        }
    }
}
