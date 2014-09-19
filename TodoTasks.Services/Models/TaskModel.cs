namespace TodoTasks.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web;
    using TodoTasks.Models;

    public class TaskModel
    {
        public static Expression<Func<TodoTask, TaskModel>> FromTask
        {
            get
            {
                return t => new TaskModel
                {
                    Id = t.Id,
                    Content = t.Content,
                    CreationDate = t.CreationDate,
                    Deadline = t.Deadline,
                    Status = t.Status,
                    CategoryId = t.CategoryId
                };
            }
        }

        public TaskModel()
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
    }
}