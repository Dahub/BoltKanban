using MediatR;
using Kanban.Domain.Entities;
using Kanban.Application.DTOs;
using Kanban.Domain.Interfaces;

namespace Kanban.Application.Queries;

public record GetTasksQuery : IRequest<IEnumerable<TaskDto>>;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync();
        return tasks.Select(t => new TaskDto(
            t.Id,
            t.Title,
            t.Description,
            t.Status,
            t.IsArchived,
            t.CreatedAt,
            t.UpdatedAt
        ));
    }
}