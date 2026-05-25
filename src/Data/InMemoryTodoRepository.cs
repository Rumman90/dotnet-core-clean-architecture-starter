using System.Collections.Concurrent;
using Services.Todos;
using Domain.Entities;

namespace Data;

public sealed class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<int, TodoItem> todos = new();

    public InMemoryTodoRepository()
    {
        AddSeed(TodoItem.Create(1, "Review clean architecture boundaries"));
        var completed = TodoItem.Create(2, "Run the API locally");
        completed.MarkCompleted();
        AddSeed(completed);
    }

    public Task<IReadOnlyCollection<TodoItem>> ListAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyCollection<TodoItem>>(todos.Values.ToArray());
    }

    public Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        todos.TryGetValue(id, out var todo);
        return Task.FromResult(todo);
    }

    public Task<int> NextIdAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var nextId = todos.Keys.DefaultIfEmpty(0).Max() + 1;
        return Task.FromResult(nextId);
    }

    public Task AddAsync(TodoItem todo, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        todos[todo.Id] = todo;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TodoItem todo, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        todos[todo.Id] = todo;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TodoItem todo, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        todos.TryRemove(todo.Id, out _);
        return Task.CompletedTask;
    }

    private void AddSeed(TodoItem todo)
    {
        todos[todo.Id] = todo;
    }
}
