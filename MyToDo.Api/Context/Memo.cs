namespace MyToDo.Api.Context
{
    public class Memo : EntityBase
    {
        public string Title { get; set; }

        public string Content { get; set; }
    }
}