namespace Kanban.Domain.Interfaces;

public interface ITaskRepository
{
    Task<Domain.Entities.Task> GetByIdAsync(Guid id);
    Task<IEnumerable<Domain.Entities.Task>> GetAllAsync();
    Task AddAsync(Domain.Entities.Task task);
    Task UpdateAsync(Domain.Entities.Task task);
}