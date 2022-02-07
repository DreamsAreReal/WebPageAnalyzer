namespace WebPageAnalyzer.Business;

internal class UnSubscriber<T> : IDisposable
{
    private readonly IObserver<T> _observer;
    private readonly List<IObserver<T>> _observers;

    public UnSubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
            _observers.Remove(_observer);
    }
}