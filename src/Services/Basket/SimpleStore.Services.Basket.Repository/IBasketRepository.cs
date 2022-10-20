using SimpleStore.Services.Basket.Domain;

namespace SimpleStore.Services.Basket.Repository
{
    public interface IBasketRepository
    {
        Task<bool> DeleteAsync(Guid buyerId);
        Task<CustomerBasket?> GetAsync(Guid buyerId);
        Task<CustomerBasket?> UpdateAsync(CustomerBasket basket);
    }
}