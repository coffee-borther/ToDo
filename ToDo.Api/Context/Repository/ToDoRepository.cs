using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api;
using MyToDo.Api.Context;

namespace ToDo.Api.Context.Repository
{
    public class ToDoRepository : Repository<MyToDo.Api.Context.ToDo>, IRepository<MyToDo.Api.Context.ToDo>
    {
        public ToDoRepository(MyToDoContext dbContext) : base(dbContext)
        {
            
        }
    }
}            