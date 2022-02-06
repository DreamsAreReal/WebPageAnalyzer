namespace WebPageAnalyzer.Storage.Dto;

public class TaskOutputModel
{
    public string Url { get; set; }
    public List<string> Words { get; set; }
    public string CronExpression { get; set; }
}