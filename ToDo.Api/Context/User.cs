namespace MyToDo.Api.Context
{
    public class User : EntityBase
    {
        public string Account { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
