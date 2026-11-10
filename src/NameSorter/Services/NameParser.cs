using NameSorter.Models;

namespace NameSorter.Services;

/// <summary>Converts a line of text into a validated person name.</summary>
public sealed class NameParser
{
    public PersonName Parse(string line)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(line);

        // Normalize repeated spaces and tabs so display and comparison are predictable.
        var parts = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length is < 2 or > 4)
        {
            throw new ArgumentException("A name must contain one to three given names followed by a last name.");
        }

        var lastName = parts[^1];
        lastName = char.ToUpperInvariant(lastName[0]) + lastName[1..];

        return new PersonName(parts[..^1], lastName);
    }
}
