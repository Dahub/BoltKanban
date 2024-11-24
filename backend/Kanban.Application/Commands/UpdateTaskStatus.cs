using MediatR;
using Kanban.Domain.Entities;
using Kanban.Domain.Interfaces;
using Kanban.Application.Common.Exceptions;
using Kanban.Domain.Events;

namespace Kanban.Application.Commands;

public record UpdateTaskStatusCommand(Guid TaskId, Domain.Entities.TaskStatus NewStatus) : IRequest;

public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IEventStore _eventStore;

    public UpdateTaskStatusCommandHandler(ITaskRepository taskRepository, IEventStore eventStore)
    {
        _taskRepository = taskRepository;
        _eventStore = eventStore;
    }

    public async System.Threading.Tasks.Task Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task == null) throw new NotFoundException($"Task with ID {request.TaskId} not found");

        task.UpdateStatus(request.NewStatus);
        
        var @event = new TaskStatusChangedEvent(task.Id, request.NewStatus);
        await _eventStore.SaveEventAsync(@event);
        
        await _taskRepository.UpdateAsync(task);
    }
}