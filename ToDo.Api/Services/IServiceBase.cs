using System.Threading.Tasks;
using MyToDo.Shared.Parameters;

namespace MyToDo.Api.Services
{
    public interface IServiceBase<T>
    {
        public Task<ApiResponse> GetAllAsync(QueryParameters parameters);
        public Task<ApiResponse> GetSingleAsync(int id);
        public Task<ApiResponse> AddAsync(T model);
        public Task<ApiResponse> UpdateAsync(T model);
        public Task<ApiResponse> DeleteAsync(int id);
    }
}
