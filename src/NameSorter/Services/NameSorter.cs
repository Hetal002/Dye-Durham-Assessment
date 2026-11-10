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
            .OrderBy(person => person.LastName, NameComparer)
            .ThenBy(person => string.Join(' ', person.GivenNames), NameComparer)
            // Ordinal tie-breakers make output deterministic when case differs.
            .ThenBy(person => person.LastName, StringComparer.Ordinal)
            .ThenBy(person => string.Join(' ', person.GivenNames), StringComparer.Ordinal)
            .ToArray();
    }
}
