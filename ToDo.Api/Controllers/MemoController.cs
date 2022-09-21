using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyToDo.Api.Services;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Controllers
{
    /// <summary>
    /// 备忘录控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService _iMemoService;

        public MemoController(IMemoService iMemoService)
        {
            _iMemoService = iMemoService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAsync(int id) => await _iMemoService.GetSingleAsync(id);

        [HttpGet]
        public async Task<ApiResponse> GetAllAsync([FromQuery] QueryParameters parameters) => await _iMemoService.GetAllAsync(parameters);

        [HttpPost]
        public async Task<ApiResponse> AddAsync([FromBody] MemoDto model) => await _iMemoService.AddAsync(model);

        [HttpPost]
        public async Task<ApiResponse> UpdateAsync([FromBody] MemoDto model) =>
            await _iMemoService.UpdateAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> DeleteAsync(int id) => await _iMemoService.DeleteAsync(id);
    }
}