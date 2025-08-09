using Microsoft.EntityFrameworkCore;
using TodoAppSnowlyCode.Data.Models;

namespace TodoAppSnowlyCode.Data.DbSetup
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
