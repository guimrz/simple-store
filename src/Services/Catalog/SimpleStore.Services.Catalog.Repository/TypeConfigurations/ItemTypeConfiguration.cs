using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Services.Catalog.Domain;

namespace SimpleStore.Services.Catalog.Repository.TypeConfigurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            throw new NotImplementedException();
        }
    }
}
