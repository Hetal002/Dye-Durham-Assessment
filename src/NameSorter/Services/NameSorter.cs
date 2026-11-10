using NameSorter.Models;

namespace NameSorter.Services;

/// <summary>Orders people by family name, then by their given names.</summary>
public sealed class NameSorter
{
    private static readonly StringComparer NameComparer = StringComparer.OrdinalIgnoreCase;

    
public IReadOnlyList<PersonName> Sort(IEnumerable<PersonName> people)
{
    ArgumentNullException.ThrowIfNull(people);

    return people
        .OrderBy(person => person.LastName, StringComparer.OrdinalIgnoreCase)
        .ThenBy(person => string.Join(' ', person.GivenNames),
            StringComparer.OrdinalIgnoreCase)
        // Resolve case differences in family names first.
        .ThenBy(person => person.LastName, StringComparer.Ordinal)
        // Then resolve case differences in given names.
        .ThenBy(person => string.Join(' ', person.GivenNames),
            StringComparer.Ordinal)
        .ToArray();
}

}
