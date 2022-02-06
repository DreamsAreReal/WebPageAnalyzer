using System.Text;
using WebPageAnalyzer.Analyzer.Models;

namespace WebPageAnalyzer.Analyzer;

public class WordsCountAnalyze : IAnalyzer
{
    public AbstractResult Analyze(StringBuilder stringBuilder)
    {
        return new WordsCountResult();
    }
}