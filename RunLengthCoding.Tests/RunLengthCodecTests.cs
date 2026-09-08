using RunLengthCoding;
using Xunit;

namespace RunLengthCoding.Tests;

public class RunLengthCodecTests
{
    [Theory]
    [InlineData("aaabbcccdde", "a3b2c3d2e")]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("abc", "abc")]
    [InlineData("aabbcc", "a2b2c2")]
    [InlineData("aabbbcccc", "a2b3c4")]
    public void Compress_ReturnsExpectedResult(string input, string expected)
    {
        string actual = RunLengthCodec.Compress(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("a3b2c3d2e", "aaabbcccdde")]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("abc", "abc")]
    public void Decompress_ReturnsExpectedResult(string input, string expected)
    {
        string actual = RunLengthCodec.Decompress(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Compress_HandlesRunsLongerThanNineCharacters()
    {
        string input = new string('a', 12);

        string actual = RunLengthCodec.Compress(input);

        Assert.Equal("a12", actual);
    }

    [Fact]
    public void Decompress_HandlesMultiDigitCounts()
    {
        string actual = RunLengthCodec.Decompress("a12");

        Assert.Equal(new string('a', 12), actual);
    }

    [Theory]
    [InlineData("aaabbcccdde")]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("abcdefgh")]
    [InlineData("zzzzzzzzzzzz")]
    [InlineData("aabbaabbaa")]
    public void Decompress_IsInverseOfCompress(string original)
    {
        string compressed = RunLengthCodec.Compress(original);

        string roundTripped = RunLengthCodec.Decompress(compressed);

        Assert.Equal(original, roundTripped);
    }

    [Fact]
    public void Compress_ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => RunLengthCodec.Compress(null!));
    }

    [Fact]
    public void Decompress_ThrowsOnNull()
    {
        Assert.Throws<ArgumentNullException>(() => RunLengthCodec.Decompress(null!));
    }

    [Theory]
    [InlineData("Abc")]
    [InlineData("abc1")]
    [InlineData("abc ")]
    public void Compress_ThrowsOnInvalidCharacters(string input)
    {
        Assert.Throws<ArgumentException>(() => RunLengthCodec.Compress(input));
    }
}
