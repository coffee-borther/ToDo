using System;

namespace MyToDo.Api.Context
{
    public class ToDo : EntityBase

    {
        public string Title { get; set; }

        public string Content { get; set; }

        public int Status { get; set; }
    }
}
