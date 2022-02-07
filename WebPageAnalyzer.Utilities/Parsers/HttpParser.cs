using System.Text;

namespace WebPageAnalyzer.Analyzer.Parsers;

public class HttpParser : IParser
{
    private readonly HttpClient _client;

    public HttpParser()
    {
        _client = new HttpClient();
    }

    public async Task<StringBuilder> Parse(string url)
    {
        var response = await _client.GetAsync(url);

        if (!response.IsSuccessStatusCode) throw new Exception($"{url} was not success answer");

        return new StringBuilder(await response.Content.ReadAsStringAsync());
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}