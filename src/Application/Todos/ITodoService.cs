using Application.Common;

namespace Application.Todos;

public interface ITodoService
{
    Task<IReadOnlyCollection<TodoDto>> ListAsync(CancellationToken cancellationToken);
    Task<TodoDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<TodoDto>> CreateAsync(CreateTodoCommand command, CancellationToken cancellationToken);
    Task<Result<TodoDto>> UpdateAsync(int id, UpdateTodoCommand command, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
}
