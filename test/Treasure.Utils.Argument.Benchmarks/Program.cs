// .\Run.ps1
// or
// dotnet run -c Release -f [net6.0 net7.0] --filter "*"

#pragma warning disable CA1050
#pragma warning disable CA1812
#pragma warning disable CA1822
#pragma warning disable CA1510
#pragma warning disable CS1591
#pragma warning disable IDE0211

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Treasure.Utils;

BenchmarkRunner.Run<Tests>(args: args);

[HideColumns("value")]
[DisassemblyDiagnoser]
public class Tests
{
    [Benchmark]
    [Arguments("foo")]
    public void ArgumentNotNull(string? value)
    {
        Argument.NotNull(value);
    }

    [Benchmark]
    [Arguments("foo")]
    public void ArgumentNullExceptionThrowIfNull(string? value)
    {
        ArgumentNullException.ThrowIfNull(value);
    }

    [Benchmark]
    [Arguments("foo")]
    public void IfValueIsNullThrowArgumentNullException(string? value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
    }

    [Benchmark]
    [Arguments("foo")]
    public void ArgumentNotNullOrEmpty(string? value)
    {
        Argument.NotNullOrEmpty(value);
    }

#if NET7_0_OR_GREATER
    [Benchmark]
    [Arguments("foo")]
    public void ArgumentExceptionThrowIfNullOrEmpty(string? value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
    }
#endif

    [Benchmark]
    [Arguments("foo")]
    public void IfValueIsNullOrEmptyThrowArgumentException(string? value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(value);
        }

        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(value));
        }
    }

    [Benchmark]
    [Arguments("foo")]
    public void ArgumentNotNullOrWhiteSpace(string? value)
    {
        Argument.NotNullOrWhiteSpace(value);
    }

#if NET8_0_OR_GREATER
    [Benchmark]
    [Arguments("foo")]
    public void ArgumentExceptionThrowIfNullOrWhiteSpace(string? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
    }
#endif

    [Benchmark]
    [Arguments("foo")]
    public void IfValueIsNullOrWhiteSpaceThrowArgumentException(string? value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(value);
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or empty.", nameof(value));
        }
    }
}
