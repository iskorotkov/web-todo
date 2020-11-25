using Microsoft.EntityFrameworkCore;
using WebTodo.Models;

namespace WebTodo.Db
{
    public sealed class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
