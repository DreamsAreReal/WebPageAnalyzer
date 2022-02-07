using System.Text;

namespace WebPageAnalyzer.Analyzer.TextProcessors;

public class RegisterTextProcessor : ITextProcessor
{
    /// <summary>
    ///     Register to lower
    /// </summary>
    /// <param name="stringBuilder"></param>
    /// <returns></returns>
    public StringBuilder Process(StringBuilder stringBuilder)
    {
        return new StringBuilder(stringBuilder.ToString().ToLower());
    }
}