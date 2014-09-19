namespace TodoTasks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<TodoTask> tasks;

        public Category()
        {
            this.tasks = new HashSet<TodoTask>();
            
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<TodoTask> Tasks 
        {
            get
            {
                return this.tasks;
            }

            set
            {
                this.tasks = value;
            }
        }

        public string UserId { get; set; }
    }
}
