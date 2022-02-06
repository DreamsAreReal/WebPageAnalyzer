using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Observers;

public class TaskToDatabaseAppendObserver : IObserver<TaskDto>
{
    private TaskDto _model;
    private Repository<TaskDto> _repository;

    public TaskToDatabaseAppendObserver(Repository<TaskDto> repository)
    {
        _repository = repository;
    }
    
    public void OnCompleted()
    {
        Console.WriteLine($"{_model.Url} was added to database");
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