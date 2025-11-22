using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    // Demo ke liye in-memory static list
    private static readonly List<Todo> Todos = new()
    {
        new Todo { Id = 1, Title = "Sample todo 1", Completed = false },
        new Todo { Id = 2, Title = "Sample todo 2", Completed = true }
    };

    // GET api/todos
    [HttpGet]
    public ActionResult<IEnumerable<Todo>> Get()
    {
        return Ok(Todos);
    }

    // GET api/todos/1
    [HttpGet("{id:int}")]
    public ActionResult<Todo> GetById(int id)
    {
        var todo = Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    // POST api/todos
    [HttpPost]
    public ActionResult<Todo> Create([FromBody] CreateTodoRequest request)
    {
        var nextId = Todos.Any() ? Todos.Max(t => t.Id) + 1 : 1;
        var todo = new Todo
        {
            Id = nextId,
            Title = request.Title,
            Completed = false
        };

        Todos.Add(todo);

        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    // PUT api/todos/1
    [HttpPut("{id:int}")]
    public ActionResult<Todo> Update(int id, [FromBody] UpdateTodoRequest request)
    {
        var todo = Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        if (!string.IsNullOrWhiteSpace(request.Title))
            todo.Title = request.Title;

        if (request.Completed.HasValue)
            todo.Completed = request.Completed.Value;

        return Ok(todo);
    }

    // DELETE api/todos/1
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var todo = Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        Todos.Remove(todo);
        return NoContent();
    }

    // DTOs for requests
    public class CreateTodoRequest
    {
        public string Title { get; set; } = string.Empty;
    }

    public class UpdateTodoRequest
    {
        public string? Title { get; set; }
        public bool? Completed { get; set; }
    }
}
