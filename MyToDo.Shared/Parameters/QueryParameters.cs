using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Parameters
{
    public class QueryParameters
    {
        public string SearchString { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
