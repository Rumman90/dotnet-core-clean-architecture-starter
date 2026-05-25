using Microsoft.AspNetCore.Mvc;
using Services.Todos;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService todoService;

    public TodosController(ITodoService todoService)
    {
        this.todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<TodoDto>>> Get(CancellationToken cancellationToken)
    {
        var todos = await todoService.ListAsync(cancellationToken);
        return Ok(todos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var todo = await todoService.GetByIdAsync(id, cancellationToken);
        if (todo is null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> Create(
        [FromBody] CreateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var result = await todoService.CreateAsync(new CreateTodoCommand(request.Title), cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(new ProblemDetails { Title = result.Error });
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TodoDto>> Update(
        int id,
        [FromBody] UpdateTodoRequest request,
        CancellationToken cancellationToken)
    {
        var result = await todoService.UpdateAsync(
            id,
            new UpdateTodoCommand(request.Title, request.Completed),
            cancellationToken);

        if (!result.IsSuccess && result.Error == "Todo was not found.")
        {
            return NotFound();
        }

        if (!result.IsSuccess)
        {
            return BadRequest(new ProblemDetails { Title = result.Error });
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await todoService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
        {
            return NotFound();
        }

        return NoContent();
    }

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
