namespace Kanban.Application.DTOs;

public record TaskDto(
    Guid Id,
    string Title,
    string Description,
    Kanban.Domain.Entities.TaskStatus Status,
    bool IsArchived,
    DateTime CreatedAt,
    DateTime UpdatedAt
);