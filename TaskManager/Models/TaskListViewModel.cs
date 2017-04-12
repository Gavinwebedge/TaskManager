using System;

namespace TaskManager.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TaskListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Task List Name")]
        [Required]
        public string TaskName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Owner")]
        public Guid OwnerId { get; set; }
    }
}