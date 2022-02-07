using System.Text;
using System.Text.RegularExpressions;

namespace WebPageAnalyzer.Analyzer.TextProcessors;

public class OnlyWordsProcessor : ITextProcessor
{
    /// <summary>
    ///     Only words, remove all special symbols.
    ///     Only for Cyrillic and latin
    /// </summary>
    /// <param name="stringBuilder"></param>
    /// <returns></returns>
    public StringBuilder Process(StringBuilder stringBuilder)
    {
        var text = stringBuilder.ToString();

        var matches = Regex.Matches(text, @"[A-z]+|[А-я]+");

        var result = new StringBuilder();

        result.AppendJoin(' ', matches);

        return result;
    }
}