namespace TaskManager.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OtherIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Task List Name")]
        [Required]
        public string TaskName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Owner")]
        public Guid OwnerId { get; set; }

        [Display(Name = "List Owner")]
        public string ListOwner { get; set; }
    }
}