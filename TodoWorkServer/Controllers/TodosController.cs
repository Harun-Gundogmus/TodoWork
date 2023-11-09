using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWorkServer.Context;
using TodoWorkServer.Models;

namespace TodoWorkServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TodosController : ControllerBase
{
    AppDbContext todoAppContext = new();


    [HttpPost]
    public IActionResult AddATodo(Todo todo)
    {
        todoAppContext.Todos.Add(todo);
        todoAppContext.SaveChanges();
        //var Todos = todoAppContext.Todos.ToList();
        return Ok(todoAppContext.Todos.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult RemoveATodo(int id)
    {
        var todoId = todoAppContext.Todos.Find(id);
        todoAppContext.Todos.Remove(todoId);
        todoAppContext.SaveChanges();
        return Ok(todoAppContext.Todos.ToList());
    }

    [HttpPost]
    public IActionResult UpdateATodo(Todo todo)
    {
        todoAppContext.Todos.Update(todo);
        todoAppContext.SaveChanges();
        return Ok(todoAppContext.Todos.ToList());
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        var Todos = todoAppContext.Todos.ToList();
        return Ok(Todos);
    }

    [HttpGet("{id}")] 
    public IActionResult ChangeCompleted(int id)
    {
        var Todos = todoAppContext.Todos.ToList();
        Todos.Where(p => p.Id == id).FirstOrDefault().IsCompleted = !Todos.Where(p => p.Id == id).FirstOrDefault().IsCompleted;
        todoAppContext.SaveChanges();
        return Ok(Todos);
    }
}


