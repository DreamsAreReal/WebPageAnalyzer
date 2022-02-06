using System.Text;

namespace WebPageAnalyzer.Analyzer.TextProcessors;

public interface ITextProcessor
{
    StringBuilder Process(StringBuilder stringBuilder);
}