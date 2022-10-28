using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleStore.Core.Domain.Abstractions;
using SimpleStore.Core.Helpers;

namespace SimpleStore.Core.EntityFrameworkCore.Interceptors
{
    public class TimeTrackableInterceptor : IChangeTrackerInterceptor
    {
        DateTime date = DateTime.UtcNow;

        public void Intercept(EntityEntry entry)
        {
            if (entry.Entity != null && entry.Entity is ITimeTrackable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ReflectionHelper.SetRuntimePropertyValue((entry.Entity as ITimeTrackable)!, nameof(ITimeTrackable.CreationDate), date);
                        break;

                    case EntityState.Modified:
                        ReflectionHelper.SetRuntimePropertyValue((entry.Entity as ITimeTrackable)!, nameof(ITimeTrackable.UpdateDate), date);
                        break;
                }
            }
        }
    }
}
