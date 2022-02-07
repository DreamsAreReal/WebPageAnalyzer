using Microsoft.Extensions.Logging;

namespace WebPageAnalyzer.Business.Observers;

public class TaskFromWorkersRemoveObserver : IObserver<string>
{
    private readonly JobFactory _jobFactory;
    private string _url;
    private ILogger<TaskFromWorkersRemoveObserver> _logger;

    public TaskFromWorkersRemoveObserver(JobFactory jobFactory, ILogger<TaskFromWorkersRemoveObserver> logger)
    {
        _jobFactory = jobFactory;
        _logger = logger;
    }

    public void OnCompleted()
    {
        _logger.LogInformation($"{_url} was deleted from workers");
    }

    public void OnError(Exception error)
    {
        throw error;
    }

    public async void OnNext(string value)
    {
        _url = value;
        _jobFactory.Remove(value);
    }
}