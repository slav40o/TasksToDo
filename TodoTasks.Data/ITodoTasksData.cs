namespace TodoTasks.Data
{
    using TodoTasks.Data.Repositories;
    using TodoTasks.Models;

    public interface ITodoTasksData
    {
        IRepository<TodoTask> Tasks { get; }

        IRepository<Category> Categories { get; }

        IRepository<ApplicationUser> Users { get; }

        void SaveChanges();
    }
}
