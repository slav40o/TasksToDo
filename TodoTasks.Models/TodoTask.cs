namespace TodoTasks.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TodoTask
    {
        public TodoTask()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? Deadline { get; set; }

        public StatusType Status { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
