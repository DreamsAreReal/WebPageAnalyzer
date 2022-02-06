using WebPageAnalyzer.Business.Jobs;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Observers;

public class TaskToWorkersAppendObserver : IObserver<TaskDto>
{
    private TaskDto _model;
    private JobFactory _jobFactory;

    public TaskToWorkersAppendObserver(JobFactory jobFactory)
    {
        _jobFactory = jobFactory;
    }
    
    public void OnCompleted()
    {
        Console.WriteLine($"{_model.Url} was added to workers");
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