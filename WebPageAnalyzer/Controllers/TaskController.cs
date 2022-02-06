using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebPageAnalyzer.Business;
using WebPageAnalyzer.Storage;
using WebPageAnalyzer.Storage.Dto;

namespace WebPageAnalyzer.Controllers;

[Route("api/tasks")]
public class TaskController : Controller
{
    private Repository<TaskDto> _repository;
    private IMapper _mapper;
    private INotification<TaskDto> _notificationAppend;
    private INotification<string> _notificationRemove;
    private JobFactory _jobFactory;
    
    public TaskController(
        Repository<TaskDto> repository, 
        IMapper mapper, 
        Notification<TaskDto> notificationAppend, 
        Notification<string> notificationRemove
    )
    {
        _notificationAppend = notificationAppend;
        _notificationRemove = notificationRemove;
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
    public IActionResult Add(TaskInputModel model)
    {
        var dto = _mapper.Map<TaskDto>(model);
        _notificationAppend.Send(dto);
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Remove(string url)
    {
        return Ok();
    }
}