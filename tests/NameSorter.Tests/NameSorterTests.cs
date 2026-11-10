using NameSorter.Services;

namespace NameSorter.Tests;

public class NameSorterTests
{
    private readonly NameParser _parser = new();
    private readonly NameSorter.Services.NameSorter _sorter = new();

    [Fact]
    public void Sort_UsesLastNameThenGivenNames()
    {
        var input = new[]
        {
            "Janet Parsons", "Vaughn Lewis", "Adonis Julius Archer", "Shelby Nathan Yoder",
            "Marin Alvarez", "London Lindsey", "Beau Tristan Bentley", "Leo Gardner",
            "Hunter Uriah Mathew Clarke", "Mikayla Lopez", "Frankie Conner Ritter"
        }.Select(_parser.Parse);

        var actual = _sorter.Sort(input).Select(person => person.ToString()).ToArray();

        Assert.Equal(new[]
        {
            "Marin Alvarez", "Adonis Julius Archer", "Beau Tristan Bentley",
            "Hunter Uriah Mathew Clarke", "Leo Gardner", "Vaughn Lewis",
            "London Lindsey", "Mikayla Lopez", "Janet Parsons",
            "Frankie Conner Ritter", "Shelby Nathan Yoder"
        }, actual);
    }

    [Fact]
    public void Sort_IsCaseInsensitiveAndDeterministic()
    {
        var input = new[] { "zoe Smith", "Amy smith", "amy Smith" }.Select(_parser.Parse);
        var actual = _sorter.Sort(input).Select(person => person.ToString()).ToArray();
        Assert.Equal(new[] { "Amy Smith", "amy Smith", "zoe Smith" }, actual);
    }

    [Fact]
    public void Sort_RejectsNullCollection() => Assert.Throws<ArgumentNullException>(() => _sorter.Sort(null!));
}
