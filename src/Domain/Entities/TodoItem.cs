using Domain.Common;

namespace Domain.Entities;

public sealed class TodoItem
{
    private TodoItem(int id, string title, bool completed)
    {
        Id = id;
        Title = title;
        Completed = completed;
    }

    public int Id { get; }
    public string Title { get; private set; }
    public bool Completed { get; private set; }

    public static TodoItem Create(int id, string title)
    {
        if (id <= 0)
        {
            throw new DomainException("Todo id must be greater than zero.");
        }

        return new TodoItem(id, NormalizeTitle(title), completed: false);
    }

    public void Rename(string title)
    {
        Title = NormalizeTitle(title);
    }

    public void MarkCompleted()
    {
        Completed = true;
    }

    public void MarkIncomplete()
    {
        Completed = false;
    }

    private static string NormalizeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Todo title is required.");
        }

        var normalized = title.Trim();
        if (normalized.Length > 120)
        {
            throw new DomainException("Todo title must be 120 characters or fewer.");
        }

        return normalized;
    }
}
