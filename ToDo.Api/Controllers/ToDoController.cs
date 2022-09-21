using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Services;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// 待办事项控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _iToDoService;

        public ToDoController(IToDoService iToDoService)
        {
            _iToDoService = iToDoService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAsync(int id) => await _iToDoService.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAllAsync([FromQuery] ToDoParameters parameters) =>
            await _iToDoService.GetAllAsync(parameters);

        [HttpGet]
        public async Task<ApiResponse> Summary() => await _iToDoService.summary();

        [HttpPost]
        public async Task<ApiResponse> AddAsync([FromBody] ToDoDto model) => await _iToDoService.AddAsync(model);

        [HttpPost]
        public async Task<ApiResponse> UpdateAsync([FromBody] ToDoDto model) => await _iToDoService.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> DeleteAsync(int id) => await _iToDoService.DeleteAsync(id);
        
    }
}