namespace TodoTasks.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using TodoTasks.Models;

    public class CategoryModel
    {
        public static Expression<Func<Category, CategoryModel>> FromCategory
        {
            get
            {
                return c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name
                };
            }
        }

        public CategoryModel()
        {
            //this.Id = Guid.NewGuid();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}