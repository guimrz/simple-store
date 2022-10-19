using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Repository.TypeConfigurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Items").HasKey(p => p.Id);

            builder.Property(item => item.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(item => item.Description);

            builder.Property(item => item.CreationDate)
                .IsRequired();

            builder.Ignore(item => item.Brand);

            builder.HasOne(item => item.Brand)
                .WithMany()
                .HasForeignKey(item => item.BrandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
