using Services.Common;
using Domain.Common;
using Domain.Entities;

namespace Services.Todos;

public sealed class TodoService : ITodoService
{
    private readonly ITodoRepository repository;

    public TodoService(ITodoRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IReadOnlyCollection<TodoDto>> ListAsync(CancellationToken cancellationToken)
    {
        var todos = await repository.ListAsync(cancellationToken);
        return todos
            .OrderBy(todo => todo.Id)
            .Select(MapToDto)
            .ToArray();
    }

    public async Task<TodoDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(id, cancellationToken);
        return todo is null ? null : MapToDto(todo);
    }

    public async Task<Result<TodoDto>> CreateAsync(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var nextId = await repository.NextIdAsync(cancellationToken);
            var todo = TodoItem.Create(nextId, command.Title);

            await repository.AddAsync(todo, cancellationToken);
            return Result<TodoDto>.Success(MapToDto(todo));
        }
        catch (DomainException exception)
        {
            return Result<TodoDto>.Failure(exception.Message);
        }
    }

    public async Task<Result<TodoDto>> UpdateAsync(int id, UpdateTodoCommand command, CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(id, cancellationToken);
        if (todo is null)
        {
            return Result<TodoDto>.Failure("Todo was not found.");
        }

        try
        {
            if (command.Title is not null)
            {
                todo.Rename(command.Title);
            }

            if (command.Completed is true)
            {
                todo.MarkCompleted();
            }
            else if (command.Completed is false)
            {
                todo.MarkIncomplete();
            }

            await repository.UpdateAsync(todo, cancellationToken);
            return Result<TodoDto>.Success(MapToDto(todo));
        }
        catch (DomainException exception)
        {
            return Result<TodoDto>.Failure(exception.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var todo = await repository.GetByIdAsync(id, cancellationToken);
        if (todo is null)
        {
            return Result.Failure("Todo was not found.");
        }

        await repository.DeleteAsync(todo, cancellationToken);
        return Result.Success();
    }

    private static TodoDto MapToDto(TodoItem todo)
    {
        return new TodoDto(todo.Id, todo.Title, todo.Completed);
    }
}
