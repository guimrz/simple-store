namespace SimpleStore.Services.Basket.Application.Responses
{
    public class CustomerBasketResponse
    {
        public IEnumerable<CustomerBasketItemResponse> Products { get; set; } = new List<CustomerBasketItemResponse>();
    }
}
