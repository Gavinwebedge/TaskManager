using System.Data.Entity;
using TaskManager.Models;

namespace TaskManager
{
    public class TaskContext : DbContext
    {
        public DbSet<TaskList> TaskLists { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}