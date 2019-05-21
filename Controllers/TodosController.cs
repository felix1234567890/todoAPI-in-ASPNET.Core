using System.Collections.Generic;
using TodoAPI.Models;
using TodoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodosController : ControllerBase
  {
    private readonly TodoService _todoService;
    public TodosController(TodoService todoService)
    {
      _todoService = todoService;
    }
    [HttpGet]
    public ActionResult<List<Todo>> GetTodos()
    {
      return _todoService.GetTodos();
    }
    [HttpGet("{id:length(24)}", Name = "GetTodo")]
    public ActionResult<Todo> GetTodo(string id)
    {
      var todo = _todoService.GetTodo(id);
      if (todo == null)
      {
        return NotFound();
      }
      return todo;
    }
    [HttpPost]
    public ActionResult<Todo> CreateTodo(Todo todo)
    {
      _todoService.CreateTodo(todo);
      return CreatedAtRoute("GetTodo", new { id = todo.Id.ToString() }, todo);
    }
    [HttpPut("{id:length(24)}")]
    public IActionResult UpdateTodo(string id, Todo newTodo)
    {
      var todo = _todoService.GetTodo(id);
      if (todo == null)
      {
        return NotFound();
      }
      _todoService.UpdateTodo(id, newTodo);
      return NoContent();
    }
    [HttpDelete("{id:length(24)}")]
    public IActionResult DeleteTodo(string id)
    {
      var todo = _todoService.GetTodo(id);
      if (todo == null)
      {
        return NotFound();
      }
      _todoService.DeleteTodo(id);
      return NoContent();
    }
  }
}