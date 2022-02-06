using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;

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
        var result = new StringBuilder();
        
        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(stringBuilder.ToString());
        
        // Remove script & style nodes
        document.DocumentNode.Descendants().Where( n => n.Name == "script" || n.Name == "style" ).ToList().ForEach(n => n.Remove());
        
        foreach (HtmlNode node in document.DocumentNode.SelectNodes("//text()[normalize-space(.) != '']"))
        {
            result.Append($"{node.InnerText} ");
        }

        var text = HttpUtility.HtmlDecode(result.ToString());
        return new StringBuilder(text);
    }
}