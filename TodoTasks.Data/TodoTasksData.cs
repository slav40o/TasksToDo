namespace TodoTasks.Data
{
    using System;
    using System.Collections.Generic;

    using TodoTasks.Data;
    using TodoTasks.Data.Repositories;
    using TodoTasks.Models;

    public class TodoTasksData : ITodoTasksData
    {
        private ITodoTasksDbContext context;
        private IDictionary<Type, object> repositories;

        public TodoTasksData()
            : this(new TodoTasksDbContext())
        {
        }

        public TodoTasksData(ITodoTasksDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<TodoTask> Tasks
        {
            get
            {
                return this.GetRepository<TodoTask>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(Repository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
