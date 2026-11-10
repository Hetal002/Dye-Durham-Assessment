# Name Sorter

A small .NET 8 console application for the Dye & Durham Name Sorter assessment. It sorts names by family name, then by given name(s), using case-insensitive alphabetical comparison. Ordinal tie-breakers make casing differences deterministic.

## Requirements

- .NET 8 SDK

## Build and test

From this directory:

```bash
dotnet restore NameSorter.sln
dotnet build NameSorter.sln --configuration Release
dotnet test NameSorter.sln --configuration Release
```

## Run

```bash
dotnet run --project src/NameSorter -- ./unsorted-names-list.txt
```

Or run the built executable with one input-file argument. The input file should contain one name per line. Each name must contain one to three given names followed by a last name. Blank lines are ignored. The sorted names are printed to standard output and written/overwritten in `sorted-names-list.txt` in the current working directory.

## Design

- `PersonName` models a validated name.
- `NameParser` parses and validates input lines.
- `NameSorter` owns sorting behavior and is independently unit tested.
- `Program` coordinates command-line input, console output, and file output.
- xUnit tests cover parsing, invalid input, whitespace normalization, sorting order, case handling, and null input.

The GitHub Actions workflow in `.github/workflows/dotnet.yml` restores, builds, and tests the solution on pushes and pull requests.
