using Microsoft.EntityFrameworkCore;

namespace MyToDo.Api.Context
{
    public class MyToDoContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; } 
        public DbSet<Memo> Memos { get; set; } 
        public DbSet<User> Users { get; set; }


        public MyToDoContext(DbContextOptions<MyToDoContext> options):base(options)
        {
            
        }
    }
}
