using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Repository.TypeConfigurations
{
    public class BrandTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            throw new NotImplementedException();
        }
    }
}
