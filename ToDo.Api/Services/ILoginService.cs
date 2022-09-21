using System.Threading.Tasks;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Services
{
    public interface ILoginService
    {
        public Task<ApiResponse> Login(string username, string password);

        public Task<ApiResponse> Register(UserDto userDto);
    }
}
