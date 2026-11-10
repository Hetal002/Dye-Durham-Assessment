using NameSorter.Services;

namespace NameSorter.Tests;

public class NameParserTests
{
    private readonly NameParser _parser = new();

    [Theory]
    [InlineData("Janet Parsons", "Janet", "Parsons")]
    [InlineData("Adonis Julius Archer", "Adonis Julius", "Archer")]
    [InlineData("Hunter Uriah Mathew Clarke", "Hunter Uriah Mathew", "Clarke")]
    public void Parse_SeparatesGivenNamesAndLastName(string input, string given, string last)
    {
        var result = _parser.Parse(input);
        Assert.Equal(given, string.Join(' ', result.GivenNames));
        Assert.Equal(last, result.LastName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("Madonna")]
    [InlineData("One Two Three Four Five")]
    public void Parse_RejectsInvalidName(string input) => Assert.ThrowsAny<ArgumentException>(() => _parser.Parse(input));

    [Fact]
    public void Parse_NormalizesWhitespace()
    {
        Assert.Equal("Adonis Julius Archer", _parser.Parse("  Adonis   Julius\tArcher ").ToString());
    }
}
