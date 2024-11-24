using Microsoft.EntityFrameworkCore;
using Kanban.Domain.Entities;
using Kanban.Infrastructure.Persistence;
using Kanban.Domain.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Kanban.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly KanbanDbContext _context;

    public TaskRepository(KanbanDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Task> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<IEnumerable<Domain.Entities.Task>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task AddAsync(Domain.Entities.Task task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Task task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
}