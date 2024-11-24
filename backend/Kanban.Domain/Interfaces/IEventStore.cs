namespace Kanban.Domain.Interfaces;

using Kanban.Domain.Events;

public interface IEventStore
{
    Task SaveEventAsync<T>(T @event) where T : TaskEvent;
}