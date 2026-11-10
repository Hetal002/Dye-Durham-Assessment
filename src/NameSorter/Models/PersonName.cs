namespace NameSorter.Models;

/// <summary>Represents a person's given name(s) and family name.</summary>
public sealed record PersonName
{
    public IReadOnlyList<string> GivenNames { get; }
    public string LastName { get; }

    public PersonName(IEnumerable<string> givenNames, string lastName)
    {
        ArgumentNullException.ThrowIfNull(givenNames);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        var names = givenNames.ToArray();
        if (names.Length is < 1 or > 3)
            throw new ArgumentException("A person must have between one and three given names.", nameof(givenNames));

        if (names.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("Given names cannot be empty.", nameof(givenNames));

        GivenNames = Array.AsReadOnly(names);
        LastName = lastName;
    }

    public override string ToString() => $"{string.Join(' ', GivenNames)} {LastName}";
}
