using Microsoft.EntityFrameworkCore;
using todolist.models;

namespace todolist.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options) { }

        public DbSet<Assignment> Assignments { get; set; }
    }
}