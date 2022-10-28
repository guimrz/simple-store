using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SimpleStore.Core.EntityFrameworkCore.Interceptors
{
    public interface IChangeTrackerInterceptor
    {
        public void Intercept(EntityEntry entry);
    }
}
