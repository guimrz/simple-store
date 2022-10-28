namespace SimpleStore.Services.Catalog.Application.Responses
{
    public class ProductCategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
