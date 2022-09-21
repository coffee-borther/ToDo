using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Services;
using MyToDo.Shared.Dtos;

namespace MyToDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _iloginService;

        public LoginController(ILoginService IloginService)
        {
            _iloginService = IloginService;
        }

        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto param) =>
            await _iloginService.Login(param.Account, param.Password);

        [HttpPost]
        public async Task<ApiResponse> Register(UserDto user) => await _iloginService.Register(user);
    }
}