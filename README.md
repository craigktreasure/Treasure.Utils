# Treasure.Utils

[![CI Build](https://github.com/craigktreasure/Treasure.Utils/actions/workflows/CI.yml/badge.svg?branch=main)](https://github.com/craigktreasure/Treasure.Utils/actions/workflows/CI.yml)
[![codecov](https://codecov.io/gh/craigktreasure/Treasure.Utils/branch/main/graph/badge.svg?token=28F4PZLPN8)](https://codecov.io/gh/craigktreasure/Treasure.Utils)

This repository contains some of my common utilities.

## Treasure.Utils.Argument

> **Important**
>
> This package is now deprecated as it isn't necessary in .NET 8+.

[![NuGet](https://img.shields.io/nuget/v/Treasure.Utils.Argument)](https://www.nuget.org/packages/Treasure.Utils.Argument/)
[![NuGet](https://img.shields.io/nuget/dt/Treasure.Utils.Argument)](https://www.nuget.org/packages/Treasure.Utils.Argument/)

Utilities for working with arguments with nullable awareness.

```csharp
using Treasure.Utils;

public void SomeMethod(object? anObject, string? aString, object inheritedButUnused)
{
    Argument.MarkUsed(inheritedButUnused);
    Argument.NotNull(anObject);
    Argument.NotNullOrEmpty(aString);
    Argument.NotNullOrWhiteSpace(aString);
}
```
