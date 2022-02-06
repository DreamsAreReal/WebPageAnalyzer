namespace WebPageAnalyzer.Business;

internal class UnSubscriber<T>  : IDisposable 
{
    private readonly List<IObserver<T>> _observers;
    private readonly IObserver<T> _observer;

    public UnSubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        this._observers = observers;
        this._observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
            _observers.Remove(_observer);
    }
}