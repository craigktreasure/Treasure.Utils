namespace Treasure.Utils.Extensions;

using System.Reflection;
using System.Runtime.CompilerServices;

internal static class EnumerationExtensions
{
    private static readonly Dictionary<Type, bool> enumTypeFlagMap = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsDefined<TEnum>(this TEnum value)
        where TEnum : Enum => Enum.IsDefined(typeof(TEnum), value);

    public static bool IsFlagsEnum<TEnum>(this TEnum _)
        where TEnum : Enum
    {
        Type enumType = typeof(TEnum);
        if (enumTypeFlagMap.TryGetValue(enumType, out bool isFlagsEnum))
        {
            return isFlagsEnum;
        }

        isFlagsEnum = enumType.IsDefined(typeof(FlagsAttribute));
        enumTypeFlagMap[enumType] = isFlagsEnum;

        return isFlagsEnum;
    }
}
