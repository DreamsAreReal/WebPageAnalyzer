using System.Text;

namespace WebPageAnalyzer.Analyzer.Parsers;

public interface IParser : IDisposable
{
    Task<StringBuilder> Parse(string url);
}