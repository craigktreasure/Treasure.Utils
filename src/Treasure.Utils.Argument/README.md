# Treasure.Utils.Argument

> **Important**
>
> This package is now deprecated as it isn't necessary in .NET 8+.
> The source code remains here for reference if necessary.

[![NuGet](https://img.shields.io/nuget/v/Treasure.Utils.Argument)](https://www.nuget.org/packages/Treasure.Utils.Argument/)
[![NuGet](https://img.shields.io/nuget/dt/Treasure.Utils.Argument)](https://www.nuget.org/packages/Treasure.Utils.Argument/)

Utilities for working with arguments with nullable awareness.

```csharp
using Treasure.Utils;

public void SomeMethod(object? anObject, string? aString, object inheritedButUnused)
{
    Argument.MarkUsed(inheritedButUnused);
    this.AnObject = Argument.NotNull(anObject);
    this.AString = Argument.NotNullOrEmpty(aString);
    this.AString = Argument.NotNullOrWhiteSpace(aString);
}
```
