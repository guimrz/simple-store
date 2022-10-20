using SimpleStore.Services.Basket.Services.Products.Entities;

namespace SimpleStore.Services.Basket.Application.Services.Products
{
    public interface IProductsService
    {
        Task<IEnumerable<IProduct>> GetProductsInformationAsync(IEnumerable<Guid> products);
    }
}