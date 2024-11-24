using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Kanban.Domain.Interfaces;
using Kanban.Infrastructure.Persistence;
using Kanban.Infrastructure.Repositories;

namespace Kanban.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<KanbanDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IEventStore, EventStore.EventStore>();

        return services;
    }
}