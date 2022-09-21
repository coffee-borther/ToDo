using System;

namespace MyToDo.Api.Context
{
    public class EntityBase
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
