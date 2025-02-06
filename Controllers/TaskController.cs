using FirstApi.Dto.Task;
using FirstApi.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers;

[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskInterface _taskInterface;
    
    public TaskController(ITaskInterface taskInterface)
    {
        _taskInterface = taskInterface;
    }

    [HttpGet("tasks")]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskInterface.GetAll();
        return Ok(tasks);
    }
    
    [HttpGet("task/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskInterface.GetById(id);
        return Ok(task);
    }
    
    [HttpPost("task")]
    public async Task<IActionResult> Create(CreateTaskDto task)
    {
        var createdTask = await _taskInterface.Create(task);
        return Ok(createdTask);
    }

    [HttpPut("task/{id}")]
    public async Task<IActionResult> Update(UpdateTaskDto task, int id)
    {
        var updatedTask = await _taskInterface.Update(task, id);
        return Ok(updatedTask);
    }
    
    [HttpDelete("task/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedTask = await _taskInterface.Delete(id);
        return Ok(deletedTask);
    }
}