using CryptLearn.Modules.Languages.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptLearn.Modules.Languages.Core.DAL.Configurations;
internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(Shared.Configurations.MaxNameLength);

        builder.HasKey(x => x.Name);
    }
}
