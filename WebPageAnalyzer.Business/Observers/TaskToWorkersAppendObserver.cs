using Microsoft.Extensions.Logging;
using WebPageAnalyzer.Business.Jobs;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Observers;

public class TaskToWorkersAppendObserver : IObserver<TaskDto>
{
    private readonly JobFactory _jobFactory;
    private TaskDto _model;
    private ILogger<TaskFromDatabaseRemoveObserver> _logger;

    public TaskToWorkersAppendObserver(JobFactory jobFactory, ILogger<TaskFromDatabaseRemoveObserver> logger)
    {
        _logger = logger;
        _jobFactory = jobFactory;
    }

    public void OnCompleted()
    {
        _logger.LogInformation($"{_model.Url} was added to workers");
    }

    public void OnError(Exception error)
    {
        throw error;
    }

    public async void OnNext(TaskDto value)
    {
        _model = value;
        _jobFactory.Add<WordsCountJob>(value);
    }
}