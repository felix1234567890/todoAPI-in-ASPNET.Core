using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TodoAPI.Models;

namespace TodoAPI.Services
{
  public class TodoService
  {
    private readonly IMongoCollection<Todo> _todos;

    public TodoService(IConfiguration config)
    {
      var client = new MongoClient(config.GetConnectionString("DB"));
      var database = client.GetDatabase("TodoData");
      _todos = database.GetCollection<Todo>("Todos");
    }
    public List<Todo> GetTodos()
    {
      return _todos.Find(todo => true).ToList();
    }
    public Todo GetTodo(string id)
    {
      return _todos.Find(todo => todo.Id == id).FirstOrDefault();
    }
    public Todo CreateTodo(Todo todo)
    {
      _todos.InsertOne(todo);
      return todo;
    }
    public void UpdateTodo(string id, Todo newTodo)
    {
      _todos.ReplaceOne(todo => todo.Id == id, newTodo);
    }
    public void DeleteTodo(string id)
    {
      _todos.DeleteOne(todo => todo.Id == id);
    }
  }

}