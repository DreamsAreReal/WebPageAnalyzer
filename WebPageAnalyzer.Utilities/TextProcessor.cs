using System.Text;
using WebPageAnalyzer.Analyzer.TextProcessors;

namespace WebPageAnalyzer.Analyzer;

public class TextProcessor
{
    public StringBuilder Process(ITextProcessor[] processors, StringBuilder stringBuilder)
    {
        foreach (var processor in processors) stringBuilder = processor.Process(stringBuilder);

        return stringBuilder;
    }
}