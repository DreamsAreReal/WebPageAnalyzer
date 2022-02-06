using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Observers;

public class TaskFromWorkersRemoveObserver : IObserver<string>
{
    private string _url;
    private JobFactory _jobFactory;

    public TaskFromWorkersRemoveObserver(JobFactory jobFactory)
    {
        _jobFactory = jobFactory;
    }
    
    public void OnCompleted()
    {
        Console.WriteLine($"{_url} was deleted from workers");
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