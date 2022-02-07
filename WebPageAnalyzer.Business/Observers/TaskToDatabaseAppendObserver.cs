using Microsoft.Extensions.Logging;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Observers;

public class TaskToDatabaseAppendObserver : IObserver<TaskDto>
{
    private TaskDto _model;
    private readonly Repository<TaskDto> _repository;
    private ILogger<TaskToDatabaseAppendObserver> _logger;

    public TaskToDatabaseAppendObserver(Repository<TaskDto> repository, ILogger<TaskToDatabaseAppendObserver> logger)
    {
        _logger = logger;
        _repository = repository;
    }

    public void OnCompleted()
    {
        _logger.LogInformation($"{_model.Url} was added to database");
    }

    public void OnError(Exception error)
    {
        throw error;
    }

    public async void OnNext(TaskDto value)
    {
        _model = value;
        await _repository.Add(value);
    }
}