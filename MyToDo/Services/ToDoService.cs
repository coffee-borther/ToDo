using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Common.Models;
using MyToDo.Shared;
using MyToDo.Shared.Annotations;
using MyToDo.Shared.Contact;
using MyToDo.Shared.Parameters;
using MyToDo.Services;
using MyToDo.Shared.Dtos;
using ToDoDto = MyToDo.Shared.Dtos.ToDoDto;

namespace MyToDo.Services
{
    public class ToDoService : ServiceBase<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient client;

        public ToDoService([NotNull] HttpRestClient client) : base(client, "ToDo")
        {
            this.client = client;
        }

        public async Task<ApiResponse<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameters parameter)
        {
            RequestBase request = new RequestBase();
            request.Method = RestSharp.Method.GET;
            request.Route = $"api/ToDo/GetAll?pageIndex={parameter.PageIndex}" +
                            $"&pageSize={parameter.PageSize}" +
                            $"&search={parameter.SearchString}" +
                            $"&status={parameter.Status}";
            return await client.ExcuteAsync<PagedList<ToDoDto>>(request);
        }

        public async Task<ApiResponse<SummaryDto>> GetSummaryAsync()
        {
            RequestBase request = new RequestBase();
            request.Route = "api/ToDo/Summary";
            return await client.ExcuteAsync<SummaryDto>(request);
        }
    }
}