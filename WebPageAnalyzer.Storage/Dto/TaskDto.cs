namespace WebPageAnalyzer.Storage.Dto;

public class TaskDto : BaseDto
{
    public List<string> Words { get; set; }
    public string CronExpression { get; set; }
}