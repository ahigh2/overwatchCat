using CommandLine;

namespace Overwatch.CatCounter
{
    public class CounterParameters : ICounterParameters
    {
        [Value(0, Required = true, MetaName = "searchTerm", HelpText = "The search term that will be counted in the input text.")]
        public string SearchTerm { get; set; }

        [Option(shortName: 'm', longName: "mode", Required = true, HelpText = "The search mode. `lax` indicates prefix and partial word matching, `Strict` indicates exact word matching.")]
        public SearchMode Mode { get; set; }

        [Option(shortName: 'p', longName: "path", Required = false, HelpText = "The full path to a *.txt file containing the text to be searched.")]
        public string Path { get; set; }

        [Option(shortName: 'i', longName: "inputText", Required = false, HelpText = "The raw text to be searched.")]
        public string Text { get; set; }

    }

    public interface ICounterParameters
    {
        public string SearchTerm { get; set; }
        public SearchMode Mode { get; set; }
        public string Path { get; set; }
        public string Text { get; set; }
    }
}
