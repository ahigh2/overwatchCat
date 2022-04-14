# Cat Counter: A Code Exercise

This application counts the number of occurrences of an arbitrary word (in this case 'cat') in an arbitrary text file or raw string input.

## Description
This application was created for a take-home exercise and serves to locate occurrences of a User-selected word in a User-selected text. The text may be either a text file (*.txt) or raw string input.
Searching occurs in one of two modes:

* Lax - Finds the character sequence provided (i.e. 'caterpillar' and 'cat' both match 'cat')
* Strict - Finds the exact word provided (i.e. 'the cat' matches 'cat' but 'caterpillar' does not)

The results of the operation are reported via Console stdout to the screen and are not persisted. The application is invoked via command line with arguments, see "Running the Application" for more details.



## Getting Started

### Dependencies

This is a .NET Core 3.1 Console Application and can be run on Windows, Linux, or macOS. Compiling the source requires the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/3.1).

### Installing

* `git clone` this repository
* `dotnet build` from the *.sln directory.

### Running the application

After building, `dotnet run Overwatch.CatCounter` from the *.sln directory to view help and command line information:

```
  -m, --mode             Required. The search mode. `lax` indicates prefix and
                         partial word matching, `Strict` indicates exact word
                         matching.

  -p, --path             The path to a *.txt file containing the text to be
                         searched.

  -i, --inputText        The raw text to be searched.

  --help                 Display this help screen.

  --version              Display version information.

  searchTerm (pos. 0)    Required. The search term that will be counted in the
                         input text.
```

#### CLI Examples
* Find the term `cat` in a simple string: `dotnet run -- "cat" -m "Strict" -i "The quick brown cat"`
* Find the character sequence `cat` in a simple string `dotnet run -- "cat" -m "Lax" -i "The quick brown cat"`
* Find the term `cat` in a given text file (local to the application directory): `dotnet run -- "cat" -m "Strict" -p "war_and_peace.txt"`


### Running the tests
After running `dotnet build`, simply run `dotnet test` to execute a series of [unit tests](https://github.com/ahigh2/overwatchCat/blob/master/Overwatch.CatCounter.Tests/WordCounterTests.cs) against the word counter and [integration tests](https://github.com/ahigh2/overwatchCat/blob/master/Overwatch.CatCounter.Tests/CatCounterApp.IntegrationTests.cs) against the entire application.

## Help

`dotnet run -- --help` for CLI information.

## Author

Aaron High (aaron.a.high@gmail.com)
