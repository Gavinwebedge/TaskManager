using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Display(Name = "Task Item Name")]
        [Required]
        public string TaskItemName { get; set; }

        public string Description { get; set; }

        public int TaskId { get; set; }

        public TaskList TaskLists { get; set; }

        [Display(Name = "Is Completed")]
        public bool IsCompleted { get; set; }
    }
}