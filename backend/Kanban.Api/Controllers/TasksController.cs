using Microsoft.AspNetCore.Mvc;
using MediatR;
using Kanban.Application.Commands;
using Kanban.Application.Queries;

namespace Kanban.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var result = await _mediator.Send(new GetTasksQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
    {
        var taskId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTasks), new { id = taskId }, null);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTaskStatusCommand command)
    {
        if (id != command.TaskId)
            return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> ArchiveTask(Guid id)
    {
        await _mediator.Send(new ArchiveTaskCommand(id));
        return NoContent();
    }
}