using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Observers;

public class TaskFromDatabaseRemoveObserver : IObserver<string>
{
    private string _url;
    private Repository<TaskDto> _repository;

    public TaskFromDatabaseRemoveObserver(Repository<TaskDto> repository)
    {
        _repository = repository;
    }
    
    public void OnCompleted()
    {
        Console.WriteLine($"{_url} was deleted from database");
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