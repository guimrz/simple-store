using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Services.Catalog.Domain;

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

            builder.HasOne(product => product.Brand)
                .WithMany()
                .HasForeignKey(product => product.BrandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(product => product.Categories)
                .WithOne(productCategory => productCategory.Product)
                .HasForeignKey(productCategory => productCategory.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Metadata.SetPropertyAccessMode(PropertyAccessMode.PreferField);
        }
    }
}