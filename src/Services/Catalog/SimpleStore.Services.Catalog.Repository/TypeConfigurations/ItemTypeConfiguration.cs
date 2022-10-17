using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Repository.TypeConfigurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items").HasKey(p => p.Id);

            builder.Property(brand => brand.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(brand => brand.Description);

            builder.Property(brand => brand.CreationDate)
                .IsRequired();
        }
    }
}
