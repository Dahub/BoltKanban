namespace Kanban.Infrastructure.EventStore;

public class EventData
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTime Timestamp { get; set; }
}