using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Kanban.Domain.Events;
using Kanban.Domain.Interfaces;
using Kanban.Infrastructure.Persistence;

namespace Kanban.Infrastructure.EventStore;

public class EventStore : IEventStore
{
    private readonly KanbanDbContext _context;

    public EventStore(KanbanDbContext context)
    {
        _context = context;
    }

    public async Task SaveEventAsync<T>(T @event) where T : TaskEvent
    {
        var eventData = new EventData
        {
            Id = Guid.NewGuid(),
            AggregateId = @event.Id,
            Type = @event.GetType().Name,
            Data = JsonSerializer.Serialize(@event),
            Timestamp = @event.Timestamp
        };

        await _context.Events.AddAsync(eventData);
        await _context.SaveChangesAsync();
    }
}