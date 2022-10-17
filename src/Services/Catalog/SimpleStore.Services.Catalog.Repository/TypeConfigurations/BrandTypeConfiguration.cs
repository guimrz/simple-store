using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Repository.TypeConfigurations
{
    public class BrandTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands").HasKey(p => p.Id);

            builder.Property(brand => brand.Name)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(brand => brand.Description);

            builder.Property(brand => brand.CreationDate)
                .IsRequired();
        }
    }
}
