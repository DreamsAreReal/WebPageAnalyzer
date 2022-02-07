namespace WebPageAnalyzer.OutputModels;

public class ResultOutputModel
{
    public Dictionary<string, int> WordsCount { get; set; }

    public DateTime CreatedAt { get; set; }
}