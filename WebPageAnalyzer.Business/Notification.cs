using Quartz;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business;

public class Notification<T> : IObservable<T>, INotification<T>
{
    private readonly List<IObserver<T>> _observers;

    public Notification()
    {
        _observers = new List<IObserver<T>>();
    }

    
    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
        
        return new UnSubscriber<T>(_observers, observer);
    }
    


    public void Send(T task)
    {
        foreach (var observer in _observers)
        {
            observer.OnNext(task);
        }
    }
    
    
    
}





