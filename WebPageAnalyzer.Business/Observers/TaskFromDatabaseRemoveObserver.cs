using Microsoft.Extensions.Logging;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Observers;

public class TaskFromDatabaseRemoveObserver : IObserver<string>
{
    private readonly Repository<TaskDto> _repository;
    private string _url;
    private ILogger<TaskFromDatabaseRemoveObserver> _logger;

    public TaskFromDatabaseRemoveObserver(Repository<TaskDto> repository, ILogger<TaskFromDatabaseRemoveObserver> logger)
    {
        _logger = logger;
        _repository = repository;
    }

    public void OnCompleted()
    {
        _logger.LogInformation($"{_url} was deleted from database");
    }

    public void OnError(Exception error)
    {
        throw error;
    }

    public async void OnNext(string value)
    {
        _url = value;
        await _repository.Remove(value);
    }
}