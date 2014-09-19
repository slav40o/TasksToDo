namespace TodoTasks.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using TodoTasks.Models;
    using System.Data.Entity;
    using TodoTasks.Data.Migrations;

    public class TodoTasksDbContext : IdentityDbContext<ApplicationUser>, ITodoTasksDbContext
    {
        public TodoTasksDbContext()
            : base("TodoTasks", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoTasksDbContext, Configuration>());
        }

        public static TodoTasksDbContext Create()
        {
            return new TodoTasksDbContext();
        }

        public virtual IDbSet<TodoTask> Tasks { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
