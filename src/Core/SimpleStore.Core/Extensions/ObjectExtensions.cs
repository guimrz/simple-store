namespace SimpleStore.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object value, string argumentName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
