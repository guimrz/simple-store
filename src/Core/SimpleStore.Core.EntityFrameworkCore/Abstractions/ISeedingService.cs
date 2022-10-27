namespace SimpleStore.Core.EntityFrameworkCore.Abstractions
{
    public interface ISeedingService
    {
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}
