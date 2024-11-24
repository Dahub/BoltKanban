using MediatR;
using Kanban.Domain.Entities;
using Kanban.Domain.Interfaces;
using Kanban.Domain.Events;

namespace Kanban.Application.Commands;

public record CreateTaskCommand(string Title, string Description) : IRequest<Guid>;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IEventStore _eventStore;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IEventStore eventStore)
    {
        _taskRepository = taskRepository;
        _eventStore = eventStore;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = Kanban.Domain.Entities.Task.Create(request.Title, request.Description);
        
        var @event = new TaskCreatedEvent(task.Id, task.Title, task.Description);
        await _eventStore.SaveEventAsync(@event);
        
        await _taskRepository.AddAsync(task);
        return task.Id;
    }
}