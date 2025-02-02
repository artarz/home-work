﻿using Microsoft.AspNetCore.Mvc;
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

        return Ok(new { Data = result.Data, HasError = false });

    }


    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] AddModel model)
    {
        ResponseResult result = await _service.InsertAsync(model);

        if (result.HasError)
            return BadRequest(result);
            
        return Ok(result);

    }

    [HttpPost("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateModel model)
    {
        ResponseResult result = await _service.UpdateAsync(model);

        if (result.HasError)
            return BadRequest(result);

        return Ok(result);

    }
    
    [HttpGet("Delete/{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        ResponseResult result = await _service.DeleteAsync(Id);

        if (result.HasError)
            return BadRequest(result);

        return Ok(result);

    }

    [HttpGet("SetCompleted/{Id}")]
    public async Task<IActionResult> SetCompleted(int Id)
    {
        ResponseResult result = await _service.SetCompletedAsync(Id);

        if (result.HasError)
            return BadRequest(result);

        return Ok(result);

    }   
    
    [HttpGet("Find/{Keywoard}")]
    public async Task<IActionResult> Find(string? keywoard)
    {
        ResponseResult result = await _service.FindAsync(keywoard);

        if (result.HasError)
            return BadRequest(result);

        return Ok(result);

    }
}