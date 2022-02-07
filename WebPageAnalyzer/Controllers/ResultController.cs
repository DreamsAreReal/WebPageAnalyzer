using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebPageAnalyzer.OutputModels;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Controllers;

[Route("api/results")]
public class ResultController : Controller
{
    private readonly IMapper _mapper;
    private readonly Repository<ResultDto> _repository;

    public ResultController(
        Repository<ResultDto> repository,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<ResultOutputModel>> Get()
    {
        var tasks = await _repository.Get();
        return _mapper.Map<IEnumerable<ResultOutputModel>>(tasks);
    }


    /// <summary>
    ///     Need to body because url has many symbols
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("id")]
    public async Task<ResultOutputModel> GetById([FromBody] UrlInputModel model)
    {
        var tasks = await _repository.Get(model.Url);
        if (tasks == null)
            throw new Exception("No content");
        return _mapper.Map<ResultOutputModel>(tasks);
    }
}