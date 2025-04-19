using Microsoft.AspNetCore.Mvc;
using TodoAPI.Models;
using TodoAPI.Services;

namespace TodoAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodosController(TodoService todoService) : ControllerBase
  {
    [HttpGet]
    public ActionResult<List<Todo>> GetTodos()
    {
      return todoService.GetTodos();
    }
    [HttpGet("{id:length(24)}", Name = "GetTodo")]
    public ActionResult<Todo> GetTodo(string id)
    {
      var todo = todoService.GetTodo(id);
      if (todo == null)
      {
        return NotFound();
      }
      return todo;
    }
    [HttpPost]
    public ActionResult<Todo> CreateTodo(Todo todo)
    {
      todoService.CreateTodo(todo);
      return CreatedAtRoute("GetTodo", new { id = todo.Id?.ToString() }, todo);
    }
    [HttpPut("{id:length(24)}")]
    public IActionResult UpdateTodo(string id, Todo newTodo)
    {
      var todo = todoService.GetTodo(id);
      if (todo == null)
      {
        return NotFound();
      }
      todoService.UpdateTodo(id, newTodo);
      return NoContent();
    }
    [HttpDelete("{id:length(24)}")]
    public IActionResult DeleteTodo(string id)
    {
      var todo = todoService.GetTodo(id);
      if (todo == null)
      {
        return NotFound();
      }
      todoService.DeleteTodo(id);
      return NoContent();
    }
  }
}