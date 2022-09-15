using Microsoft.AspNetCore.Mvc;
using YB.Service.ToDoService;
using YB.Shared.Models;

namespace YB.Todo.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly IToDoService _service;

    public TodoController(ILogger<TodoController> logger, IToDoService Service)
    {
        _logger = logger;
        _service = Service;
    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        ResponseResult result = await _service.GetAllAsync();

        if (result.HasError)
            return BadRequest(result);
            
        return Ok(result);

    }


    [HttpGet("Add")]
    public async Task<IActionResult> Add(AddModel model)
    {
        ResponseResult result = await _service.InsertAsync(model);

        if (result.HasError)
            return BadRequest(result);
            
        return Ok(result);

    }
}