namespace WebPageAnalyzer.Business;

public interface IPublisher<T>
{
    void Send(T task);
}