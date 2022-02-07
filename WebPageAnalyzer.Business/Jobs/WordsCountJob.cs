using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using Quartz;
using WebPageAnalyzer.Analyzer;
using WebPageAnalyzer.Analyzer.Parsers;
using WebPageAnalyzer.Analyzer.TextProcessors;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Business.Jobs;

public class WordsCountJob : IJob
{
    private IMapper _mapper;
    private readonly IParser _parser;
    private readonly ITextProcessor[] _processors;
    private readonly Repository<ResultDto> _repository;

    public WordsCountJob(Repository<ResultDto> repository, IMapper mapper)
    {
        _mapper = mapper;
        _parser = new HttpParser();
        _repository = repository;
        _processors = new ITextProcessor[]
        {
            new HtmlTextProcessor(),
            new RegisterTextProcessor(),
            new OnlyWordsProcessor()
        };
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var timer = new Stopwatch();
        timer.Start();
        try
        {
            var textProcessor = new TextProcessor();
            var data = context.JobDetail.JobDataMap["data"] as TaskDto;
            if (data == null)
                throw new Exception("Data null");

            Console.WriteLine($"Job started {data.Url}");

            // Parse and clear text
            var sb = textProcessor.Process(_processors, await _parser.Parse(data.Url));

            var text = sb.ToString();
            var wordsCount = new Dictionary<string, int>();

            foreach (var word in data.Words)
            {
                // Clear query
                var searchQuery = textProcessor.Process(_processors, new StringBuilder(word)).ToString();
                if (!wordsCount.Keys.Contains(searchQuery))
                    wordsCount.Add(searchQuery, new Regex($" {searchQuery.Trim()} ").Match(text).Length);
            }

            var result = new ResultDto
            {
                Url = data.Url,
                WordsCount = wordsCount
            };

            await _repository.Add(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            var timeTaken = timer.Elapsed;
            Console.WriteLine($"Job done {context.JobDetail.Key} at {timeTaken.ToString(@"m\:ss\.fff")}");
            _parser.Dispose();
            timer.Stop();
        }
    }
}