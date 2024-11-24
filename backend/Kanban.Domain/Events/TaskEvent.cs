namespace Kanban.Domain.Events;

public abstract record TaskEvent
{
    public Guid Id { get; init; }
    public DateTime Timestamp { get; init; }

    protected TaskEvent(Guid id)
    {
        Id = id;
        Timestamp = DateTime.UtcNow;
    }
}

public record TaskCreatedEvent : TaskEvent
{
    public string Title { get; init; }
    public string Description { get; init; }

    public TaskCreatedEvent(Guid id, string title, string description) : base(id)
    {
        Title = title;
        Description = description;
    }
}

public record TaskStatusChangedEvent : TaskEvent
{
    public Kanban.Domain.Entities.TaskStatus NewStatus { get; init; }

    public TaskStatusChangedEvent(Guid id, Kanban.Domain.Entities.TaskStatus newStatus) : base(id)
    {
        NewStatus = newStatus;
    }
}

public record TaskArchivedEvent : TaskEvent
{
    public TaskArchivedEvent(Guid id) : base(id) { }
}