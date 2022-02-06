namespace WebPageAnalyzer.Analyzer.Models;

public class WordsCountResult : AbstractResult
{
    public Dictionary<string, int> WordsCount { get; set; }
}