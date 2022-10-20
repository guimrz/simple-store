using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SimpleStore.Services.Basket.Domain;

namespace SimpleStore.Services.Basket.Repository
{
    public class CacheBasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;

        public CacheBasketRepository(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<CustomerBasket?> GetAsync(Guid buyerId)
        {
            var basket = await _cache.GetStringAsync(buyerId.ToString());

            if (string.IsNullOrWhiteSpace(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket)
        {
            await _cache.SetStringAsync(basket.BuyerId.ToString(), JsonConvert.SerializeObject(basket));

            return await GetAsync(basket.BuyerId);
        }

        public async Task<bool> DeleteAsync(Guid buyerId)
        {
            await _cache.RemoveAsync(buyerId.ToString());

            return true;
        }
    }
}