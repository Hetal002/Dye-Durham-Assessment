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
            throw new FormatException("A name must contain one to three given names followed by a last name.");
        }

        return new PersonName(parts[..^1], parts[^1]);
    }
}
