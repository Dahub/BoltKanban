namespace Kanban.Domain.Entities;

public class Task
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatus Status { get; private set; }
    public bool IsArchived { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Task() { }

    public static Task Create(string title, string description)
    {
        return new Task
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Status = Kanban.Domain.Entities.TaskStatus.Todo,
            IsArchived = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateStatus(TaskStatus newStatus)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Archive()
    {
        IsArchived = true;
        UpdatedAt = DateTime.UtcNow;
    }
}