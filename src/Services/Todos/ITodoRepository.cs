using Domain.Entities;

namespace Services.Todos;

public interface ITodoRepository
{
    Task<IReadOnlyCollection<TodoItem>> ListAsync(CancellationToken cancellationToken);
    Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> NextIdAsync(CancellationToken cancellationToken);
    Task AddAsync(TodoItem todo, CancellationToken cancellationToken);
    Task UpdateAsync(TodoItem todo, CancellationToken cancellationToken);
    Task DeleteAsync(TodoItem todo, CancellationToken cancellationToken);
}
