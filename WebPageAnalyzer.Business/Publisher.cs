namespace WebPageAnalyzer.Business;

public class Publisher<T> : IObservable<T>, IPublisher<T>
{
    private readonly List<IObserver<T>> _observers;

    public Publisher()
    {
        _observers = new List<IObserver<T>>();
    }


    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer)) _observers.Add(observer);

        return new UnSubscriber<T>(_observers, observer);
    }


    public void Send(T task)
    {
        foreach (var observer in _observers) observer.OnNext(task);
    }
}