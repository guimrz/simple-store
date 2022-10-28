using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Services.Catalog.Repository.Entities;

namespace SimpleStore.Services.Catalog.Repository.TypeConfigurations
{
    public class ProductCategoryTypeConfiguration : IEntityTypeConfiguration<Entities.ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories").HasKey(p => new { p.ProductId, p.CategoryId });

            builder.HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
