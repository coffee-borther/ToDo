using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace MyToDo.Services
{
    public class RequestBase
    {
        public Method Method { get; set; }
        public string ContentType { get; set; } = "application/json";

        public string Route { get; set; }

        public object Parameter { get; set; }

    }
}
