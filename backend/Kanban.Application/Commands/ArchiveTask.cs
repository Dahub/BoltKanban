using Kanban.Application.Common.Exceptions;
using Kanban.Domain.Events;
using Kanban.Domain.Interfaces;
using MediatR;

namespace Kanban.Application.Commands;

public record ArchiveTaskCommand(Guid TaskId) : IRequest;

public class ArchiveTaskCommandHandler : IRequestHandler<ArchiveTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IEventStore _eventStore;

    public ArchiveTaskCommandHandler(ITaskRepository taskRepository, IEventStore eventStore)
    {
        _taskRepository = taskRepository;
        _eventStore = eventStore;
    }

    public async Task Handle(ArchiveTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task == null) throw new NotFoundException($"Task with ID {request.TaskId} not found");

        task.Archive();
        
        var @event = new TaskArchivedEvent(task.Id);
        await _eventStore.SaveEventAsync(@event);
        
        await _taskRepository.UpdateAsync(task);
    }
}