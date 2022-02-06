using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;

namespace WebPageAnalyzer.Analyzer.TextProcessors;

public class RegisterTextProcessor : ITextProcessor
{
    /// <summary>
    /// Remove html tags
    /// </summary>
    /// <param name="stringBuilder"></param>
    /// <returns></returns>
    public StringBuilder Process(StringBuilder stringBuilder)
    {
        return new StringBuilder(stringBuilder.ToString().ToLower());
    }
}