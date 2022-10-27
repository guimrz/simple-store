using System.Runtime.CompilerServices;

namespace SimpleStore.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Validates and throws an exception if the specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentNullException">The value of {argumentName} cannot be null, empty or whitespaces.</exception>
        public static void ThrowIfNullOrWhitespaces(this string value, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"The value of {argumentName} cannot be null, empty or whitespaces.");
            }
        }
    }
}
