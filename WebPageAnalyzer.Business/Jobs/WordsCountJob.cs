using System.Text;
using AutoMapper;
using Quartz;
using WebPageAnalyzer.Analyzer;
using WebPageAnalyzer.Analyzer.Parsers;
using WebPageAnalyzer.Analyzer.TextProcessors;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;


namespace WebPageAnalyzer.Business.Jobs;

public class WordsCountJob : IJob, IDisposable
{
    private Repository<ResultDto> _repository;
    private ITextProcessor[] _processors;
    private IParser _parser;
    private IAnalyzer _analyzer;
    private IMapper _mapper;

    public WordsCountJob(Repository<ResultDto> repository, IMapper mapper)
    {
        _mapper = mapper;
        _parser = new HttpParser();
        _repository = repository;
        _processors = new ITextProcessor[]
        {
            new HtmlTextProcessor(),
        };
        _analyzer = new WordsCountAnalyze();
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var text = new TextProcessor().Process(_processors, await _parser.Parse(""));
            var result = _analyzer.Analyze(text);
            await _repository.Add(_mapper.Map<ResultDto>(result));
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


    public void Dispose()
    {
        _parser.Dispose();
    }
}