namespace Services.Todos;

public sealed record UpdateTodoCommand(string? Title, bool? Completed);
