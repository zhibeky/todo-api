using Microsoft.AspNetCore.Mvc;
using TodoListApi.Models;
using TodoListApi.Services;

namespace TodoListApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoService _service;
    public TodoController(TodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll() => _service.GetAll();
    
    [HttpGet("{id}")]
    public ActionResult<TodoItem> GetById(int id)
    {
        var item = _service.GetById(id);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public ActionResult<TodoItem> Create([FromBody] string title)
    {
        var item = _service.Create(title);
        return CreatedAtAction(nameof(GetById), new {id = item.Id}, item);
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TodoItem updated)
    {
        var success = _service.Update(id, updated.Title, updated.IsCompleted);
        return success ? NoContent() : NotFound();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _service.Delete(id);
        return success ? NoContent() : NotFound();
    }
}