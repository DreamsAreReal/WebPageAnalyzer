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
    private Repository<ResultDto> _repository;
    private ITextProcessor[] _processors;
    private IParser _parser;
    private IMapper _mapper;

    public WordsCountJob(Repository<ResultDto> repository, IMapper mapper)
    {
        _mapper = mapper;
        _parser = new HttpParser();
        _repository = repository;
        _processors = new ITextProcessor[]
        {
            new HtmlTextProcessor(),
            new RegisterTextProcessor(),
        };
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var data = context.JobDetail.JobDataMap["data"] as TaskDto;
            if (data == null)
                throw new Exception("Data null");
            
            Console.WriteLine($"Job started {data.Url}");
            var sb = new TextProcessor().Process(_processors, await _parser.Parse(data.Url));

            var text = sb.ToString();
            var wordsCount = new Dictionary<string, int>();
            
            foreach (var word in data.Words)
            {
                if (!wordsCount.Keys.Contains(word))
                {
                    wordsCount.Add(word, new Regex($" {word.ToLower().Trim()} ").Match(text).Length);
                }
            }
            
            var result = new ResultDto()
            {
                Url = data.Url,
                WordsCount = wordsCount
            };

            await _repository.Add(result);
            
            Console.WriteLine($"Job done {data.Url}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            _parser.Dispose();
        }
    }


}