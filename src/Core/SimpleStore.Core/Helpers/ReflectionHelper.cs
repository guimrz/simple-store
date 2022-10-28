using System.Reflection;

namespace SimpleStore.Core.Helpers
{
    public static class ReflectionHelper
    {
        public static void SetRuntimePropertyValue(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetRuntimeProperty(propertyName);

            var method = property?.GetSetMethod(nonPublic: true);

            method?.Invoke(obj, new[] { value });
        }
    }
}
