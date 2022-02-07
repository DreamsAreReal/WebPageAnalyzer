namespace WebPageAnalyzer.Business.Observers;

public class TaskFromWorkersRemoveObserver : IObserver<string>
{
    private readonly JobFactory _jobFactory;
    private string _url;

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