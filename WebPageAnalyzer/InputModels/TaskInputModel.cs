namespace WebPageAnalyzer.Storage.Dto;

public class TaskInputModel
{
    public string Url { get; set; }
    public List<string> Words { get; set; }
    public string CronExpression { get; set; }
}