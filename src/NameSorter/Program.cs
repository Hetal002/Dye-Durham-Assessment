using NameSorter.Services;

const string outputFileName = "sorted-names-list.txt";

if (args.Length != 1)
{
    Console.Error.WriteLine("Usage: name-sorter <input-file>");
    return 1;
}

var inputPath = args[0];
if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Error: input file was not found: {inputPath}");
    return 1;
}

try
{
    var parser = new NameParser();
    var sorter = new NameSorter.Services.NameSorter();
    var people = new List<NameSorter.Models.PersonName>();

    var lineNumber = 0;
    foreach (var line in File.ReadLines(inputPath))
    {
        lineNumber++;
        if (string.IsNullOrWhiteSpace(line))
            continue;

        try
        {
            people.Add(parser.Parse(line));
        }
        catch (FormatException exception)
        {
            throw new InvalidDataException($"Invalid name at line {lineNumber}: {exception.Message}", exception);
        }
    }

    var sortedPeople = sorter.Sort(people);
    var sortedNames = sortedPeople.Select(person => person.ToString()).ToArray();

    foreach (var name in sortedNames)
        Console.WriteLine(name);

    File.WriteAllLines(outputFileName, sortedNames);
    Console.WriteLine($"\nWrote {sortedNames.Length} names to {Path.GetFullPath(outputFileName)}");
    return 0;
}
catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
{
    Console.Error.WriteLine($"Error: {exception.Message}");
    return 1;
}
