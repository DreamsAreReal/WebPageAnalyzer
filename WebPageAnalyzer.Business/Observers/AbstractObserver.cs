using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Observers;

public abstract class AbstractObserver<T> : IObserver<T>
{
    public void OnCompleted()
    {
        // TODO: Implement log
    }

    public void OnError(Exception error)
    {
        throw error;
    }

    public virtual void OnNext(T value)
    {
        throw new NotImplementedException();
    }

    
}