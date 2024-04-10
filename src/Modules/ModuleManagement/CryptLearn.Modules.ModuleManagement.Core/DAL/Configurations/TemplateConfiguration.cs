using CryptLearn.Modules.ModuleManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptLearn.Modules.ModuleManagement.Core.DAL.Configurations
{
    internal class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Module)
                .WithMany(x => x.Templates)
                .IsRequired();

            builder.HasOne<Language>()
                .WithMany()
                .HasForeignKey(x => x.LanguageName)
                .IsRequired();

            builder.Property(x => x.Code)
                .HasMaxLength(Shared.Configurations.MaxCodeLength)
                .IsRequired();
        }
    }
}
