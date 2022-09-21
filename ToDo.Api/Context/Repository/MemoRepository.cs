using MyToDo.Api;
using MyToDo.Api.Context;

namespace ToDo.Api.Context.Repository
{
    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {
        public MemoRepository(MyToDoContext dbContext) : base(dbContext)
        {

        }
    }
}