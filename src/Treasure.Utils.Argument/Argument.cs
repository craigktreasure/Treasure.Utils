namespace Treasure.Utils;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Treasure.Utils.Extensions;

/// <summary>
/// A class containing various utilities useful for working with arguments.
/// </summary>
public static class Argument
{
    /// <summary>
    /// Asserts the enumeration value is defined and returns the value.
    /// Flag enumeration values are not asserted.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="name">The argument name.</param>
    /// <returns>The value.</returns>
    public static TEnum IsDefined<TEnum>(TEnum value, [CallerArgumentExpression(nameof(value))] string name = "")
        where TEnum : Enum
    {
        if (!value.IsFlagsEnum() && !value.IsDefined())
        {
            throw new ArgumentException($"The enumeration value is not defined: {value}.", name);
        }

        return value;
    }

    /// <summary>
    /// Marks the argument as being used.
    /// This method is useful in situations where you need to implement an API,
    /// but don't need a specific parameter and a discard won't work to please
    /// an analyzer.
    /// </summary>
    public static void MarkUsed(object? _)
    {
    }

    /// <summary>
    /// Asserts the value is not null and returns the value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value.</param>
    /// <param name="name">The argument name.</param>
    /// <returns>The value if not null.</returns>
    public static T NotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string name = "") where T : class
    {
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }

        return value;
    }

    /// <summary>
    /// Asserts the value is not null or empty and returns the value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="name">The argument name.</param>
    /// <returns><see cref="string"/>.</returns>
    public static string NotNullOrEmpty([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }

        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("The value cannot be null or empty.", name);
        }

        return value;
    }

    /// <summary>
    /// Asserts the value is not null, empty, or white-space and returns the value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="name">The argument name.</param>
    /// <returns><see cref="string"/>.</returns>
    public static string NotNullOrWhiteSpace([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("The value cannot be null, empty, or white-space.", name);
        }

        return value;
    }
}
