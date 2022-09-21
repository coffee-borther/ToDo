using System.Threading.Tasks;
using MyToDo.Shared;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Services
{
    public interface IToDoService:IServiceBase<ToDoDto>
    {
        Task<ApiResponse> GetAllAsync(ToDoParameters parameter);

        Task<ApiResponse> summary();
    }
}
