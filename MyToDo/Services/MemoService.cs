using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Common.Models;
using MyToDo.Shared;
using MyToDo.Shared.Annotations;
using MyToDo.Shared.Dtos;
using ToDoDto = MyToDo.Shared.Dtos.ToDoDto;

namespace MyToDo.Services
{
    public class MemoService : ServiceBase<MemoDto>, IMemoService
    {
        public MemoService([NotNull] HttpRestClient client) : base(client, "Memo")
        {
        }
    }
}