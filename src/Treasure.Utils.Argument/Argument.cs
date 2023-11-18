namespace Treasure.Utils;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

/// <summary>
/// A class containing various utilities useful for working with arguments.
/// </summary>
public static class Argument
{
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T NotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string name = "") where T : class
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(value, name);
#else
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }
#endif

        return value;
    }

    /// <summary>
    /// Asserts the value is not null or empty and returns the value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="name">The argument name.</param>
    /// <returns><see cref="string"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string NotNullOrEmpty([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
#if NET7_0_OR_GREATER
        ArgumentException.ThrowIfNullOrEmpty(value, name);
#else
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }

        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("The value cannot be null or empty.", name);
        }
#endif

        return value;
    }

    /// <summary>
    /// Asserts the value is not null, empty, or white-space and returns the value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="name">The argument name.</param>
    /// <returns><see cref="string"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string NotNullOrWhiteSpace([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
#if NET8_0_OR_GREATER
        ArgumentException.ThrowIfNullOrWhiteSpace(value, name);
#else
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("The value cannot be null, empty, or white-space.", name);
        }
#endif

        return value;
    }
}
