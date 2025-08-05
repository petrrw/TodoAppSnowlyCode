using TodoAppSnowlyCode.Data.DbSetup;
using TodoAppSnowlyCode.Data.Interfaces;
using TodoAppSnowlyCode.Data.Models;

namespace TodoAppSnowlyCode.Data.Repositories
{
    /// <summary>
    /// Repository for <see cref="ToDoItem"/>
    /// </summary>
    public class ToDoItemRepository : RepositoryBase<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(AppDbContext context) : base(context) { }
    }
}
