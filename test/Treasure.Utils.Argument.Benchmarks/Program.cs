// dotnet run -c Release -f net6.0 --filter "*" --runtimes net6.0 net7.0

#pragma warning disable IDE0211
#pragma warning disable CA1050
#pragma warning disable CA1812
#pragma warning disable CA1822
#pragma warning disable CS1591

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Treasure.Utils;

BenchmarkSwitcher.FromAssembly(typeof(Tests).Assembly).Run(args);

[HideColumns("value")]
[DisassemblyDiagnoser]
public class Tests
{
    [Benchmark]
    [Arguments("foo")]
    public void NotNull(string? value) => Argument.NotNull(value);

    [Benchmark]
    [Arguments("foo")]
    public void NotNullOrEmpty(string? value) => Argument.NotNullOrEmpty(value);

    [Benchmark]
    [Arguments("foo")]
    public void NotNullOrWhiteSpace(string? value) => Argument.NotNullOrWhiteSpace(value);
}
