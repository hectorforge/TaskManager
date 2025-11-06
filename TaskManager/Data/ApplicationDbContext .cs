using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = 1,
                    Title = "Tarea de ejemplo",
                    Description = "Esta es una tarea de prueba",
                    DueDate = DateTime.Now.AddDays(7),
                    AssignedUser = "Hector Julio",
                    Status = Models.TaskStatus.Pending,
                    CreatedAt = DateTime.Now
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Revisar documentación",
                    Description = "Revisar la documentación del proyecto",
                    DueDate = DateTime.Now.AddDays(3),
                    AssignedUser = "Fernando Perez",
                    Status = Models.TaskStatus.InProgress,
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
