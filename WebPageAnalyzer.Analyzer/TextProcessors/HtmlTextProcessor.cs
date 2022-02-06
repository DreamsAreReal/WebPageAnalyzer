using System.Text;
using System.Text.RegularExpressions;

namespace WebPageAnalyzer.Analyzer.TextProcessors;

public class HtmlTextProcessor : ITextProcessor
{
    /// <summary>
    /// Remove html tags
    /// </summary>
    /// <param name="stringBuilder"></param>
    /// <returns></returns>
    public StringBuilder Process(StringBuilder stringBuilder)
    {
        return new StringBuilder(Regex.Replace(stringBuilder.ToString(), "<[^>]+>", string.Empty));
    }
}