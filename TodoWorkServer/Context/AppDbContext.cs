using Microsoft.EntityFrameworkCore;
using TodoWorkServer.Models;

namespace TodoWorkServer.Context;

public class AppDbContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=LAPTOP-5ETPORJT;Initial Catalog=TodoWork_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().HasKey(p => p.Id);
        modelBuilder.Entity<Todo>().HasIndex(i => i.Work).IsUnique();

        modelBuilder.Entity<Todo>().HasData(
            new Todo { Id = 1, Work = "Get up", IsCompleted = false},
            new Todo { Id = 2, Work = "Work lesson", IsCompleted = false},
            new Todo { Id = 3, Work = "Go home", IsCompleted = false},
            new Todo { Id = 4, Work = "Take a shower", IsCompleted = false},
            new Todo { Id = 5 ,Work = "Get sleep", IsCompleted = false},
            new Todo { Id = 6 ,Work = "Check your e-mail", IsCompleted = true},
            new Todo { Id = 7 ,Work = "Brush teeth", IsCompleted = true},
            new Todo { Id = 8 ,Work = "Get to work", IsCompleted = true}
            );
    }
}
