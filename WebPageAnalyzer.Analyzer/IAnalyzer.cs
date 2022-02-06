using System.Text;
using WebPageAnalyzer.Analyzer.Models;

namespace WebPageAnalyzer.Analyzer;

public interface IAnalyzer
{
    AbstractResult Analyze(StringBuilder stringBuilder);
}