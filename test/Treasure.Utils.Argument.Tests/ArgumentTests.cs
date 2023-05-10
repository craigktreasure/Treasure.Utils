namespace Treasure.Utils.Tests;

public class ArgumentTests
{
    [Fact]
    public void IsDefined_FlagsEnum()
    {
        // Arrange
        TestFlagsEnum value = TestFlagsEnum.One | TestFlagsEnum.Two;

        // Act
        Argument.IsDefined(value);
        Argument.IsDefined(value);
    }

    [Fact]
    public void IsDefined_StandardEnum()
    {
        // Arrange
        TestStandardEnum value = TestStandardEnum.Value;

        // Act
        Argument.IsDefined(value);
    }
    [Fact]
    public void IsDefined_StandardEnum_InvalidEnumThrows()
    {
        // Arrange
        TestStandardEnum value = (TestStandardEnum)(-1);

        // Act
        Assert.Throws<ArgumentException>(nameof(value), () => Argument.IsDefined(value));
    }

    [Fact]
    public void MarkUsed()
    {
        // Arrange
        static void Method(string? useMe)
        {
            Argument.MarkUsed(useMe);
        }
        const string? value = null;

        // Act
        Method(value);
    }

    [Fact]
    public void NotNull()
    {
        // Arrange
        int[] objectValue = Array.Empty<int>();

        // Act and assert
        Argument.NotNull(objectValue);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("foo")]
    public void NotNullOrEmpty(string objectValue)
    {
        // Act and assert
        Argument.NotNullOrEmpty(objectValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void NotNullOrEmptyThrows(string? objectValue)
    {
        // Act and assert
        if (objectValue is null)
        {
            Assert.Throws<ArgumentNullException>(nameof(objectValue), () => Argument.NotNullOrEmpty(objectValue));
        }
        else
        {
            Assert.Throws<ArgumentException>(nameof(objectValue), () => Argument.NotNullOrEmpty(objectValue));
        }
    }

    [Fact]
    public void NotNullOrWhiteSpace()
    {
        // Act and assert
        Argument.NotNullOrWhiteSpace("foo");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void NotNullOrWhiteSpaceThrows(string? objectValue)
    {
        // Act and assert
        if (objectValue is null)
        {
            Assert.Throws<ArgumentNullException>(nameof(objectValue), () => Argument.NotNullOrWhiteSpace(objectValue));
        }
        else
        {
            Assert.Throws<ArgumentException>(nameof(objectValue), () => Argument.NotNullOrWhiteSpace(objectValue));
        }
    }

    [Fact]
    public void NotNullThrows()
    {
        // Arrange
        object? objectValue = null;

        // Act and assert
        Assert.Throws<ArgumentNullException>(nameof(objectValue), () => Argument.NotNull(objectValue));
    }

    [Flags]
    private enum TestFlagsEnum : short
    {
        None = 0,
        One = 1,
        Two = 2,
        Four = 4,
        Eight = 8
    };

    private enum TestStandardEnum : short
    {
        Value,
    }
}
