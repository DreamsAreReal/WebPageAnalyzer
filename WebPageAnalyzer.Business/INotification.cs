namespace WebPageAnalyzer.Business;

public interface INotification<T>
{
    void Send(T task);
}