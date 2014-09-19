namespace TodoTasks.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using TodoTasks.Models;

    public interface ITodoTasksDbContext
    {
        IDbSet<TodoTask> Tasks { get; set; }

        IDbSet<Category> Categories { get; set; }


        void SaveChanges();

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
