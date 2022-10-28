using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Services.Catalog.Domain;
using SimpleStore.Services.Catalog.Repository.Entities;

namespace SimpleStore.Services.Catalog.Repository.TypeConfigurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products")
                .HasKey(product => product.Id);

            builder.Property(product => product.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(product => product.Description);

            builder.Property(product => product.CreationDate)
                .IsRequired();

            builder.Property(product => product.Price).IsRequired();

            builder.Property(product => product.Stock).IsRequired();

            builder.HasOne(product => product.Brand)
                .WithMany()
                .HasForeignKey("BrandId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Metadata.SetPropertyAccessMode(PropertyAccessMode.PreferField);
        }
    }
}