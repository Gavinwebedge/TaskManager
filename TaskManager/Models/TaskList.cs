using System;

namespace TaskManager.Models
{
    public class TaskList
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public string Description { get; set; }

        public Guid OwnerId { get; set; }
    }
}