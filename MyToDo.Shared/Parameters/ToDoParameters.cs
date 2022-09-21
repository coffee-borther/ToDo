using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Parameters
{
    public class ToDoParameters : QueryParameters
    {
        public int? Status { get; set; }
    }
}
