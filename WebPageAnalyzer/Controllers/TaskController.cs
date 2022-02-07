using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebPageAnalyzer.Business;
using WebPageAnalyzer.Exceptions;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Controllers;

[Route("api/tasks")]
public class TaskController : Controller
{
    private JobFactory _jobFactory;
    private readonly IMapper _mapper;
    private readonly IPublisher<TaskDto> _publisherAppend;
    private readonly IPublisher<string> _publisherRemove;
    private readonly Repository<TaskDto> _repository;

    public TaskController(
        Repository<TaskDto> repository,
        IMapper mapper,
        Publisher<TaskDto> publisherAppend,
        Publisher<string> publisherRemove
    )
    {
        _publisherAppend = publisherAppend;
        _publisherRemove = publisherRemove;
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<TaskOutputModel>> Get()
    {
        var tasks = await _repository.Get();
        return _mapper.Map<IEnumerable<TaskOutputModel>>(tasks);
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TaskInputModel model)
    {
        if (!ModelState.IsValid)
            throw new ValidationException(ModelState);

        if (await _repository.Get(model.Url) != null) throw new Exception("Task at work");


        var dto = _mapper.Map<TaskDto>(model);
        _publisherAppend.Send(dto);
        return Ok();
    }


    /// <summary>
    ///     Need to body because url has many symbols
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Remove([FromBody] UrlInputModel model)
    {
        _publisherRemove.Send(model.Url);
        return Ok();
    }
}