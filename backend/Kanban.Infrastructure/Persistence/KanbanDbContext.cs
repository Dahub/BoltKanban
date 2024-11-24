using Microsoft.EntityFrameworkCore;
using Kanban.Domain.Entities;
using Kanban.Infrastructure.EventStore;

namespace Kanban.Infrastructure.Persistence;

public class KanbanDbContext : DbContext
{
    public DbSet<Kanban.Domain.Entities.Task> Tasks { get; set; }
    public DbSet<EventData> Events { get; set; }

    public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kanban.Domain.Entities.Task>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.IsArchived).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<EventData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AggregateId).IsRequired();
            entity.Property(e => e.Type).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Data).IsRequired();
            entity.Property(e => e.Timestamp).IsRequired();
            
            entity.HasIndex(e => e.AggregateId);
        });
    }
}