using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyToDo.Shared;
using MyToDo.Shared.Dtos;
using RestSharp;

namespace MyToDo.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient _client;
        private readonly string serviceName = "Login";

        public LoginService(HttpRestClient client)
        {
            _client = client;
        }
        public async Task<ApiResponse<UserDto>> LoginAsync(UserDto dto)
        {
            RequestBase request = new RequestBase();
            request.Method = Method.POST;
            request.Route = $"api/{serviceName}/Login";
            request.Parameter = dto;
            return await _client.ExcuteAsync<UserDto>(request);
        }

        public async Task<ApiResponse> RegisterAsync(UserDto dto)
        {
            RequestBase request = new RequestBase();
            request.Method = Method.POST;
            request.Route = $"api/{serviceName}/Register";
            request.Parameter = dto;
            return await _client.ExcuteAsync(request);
        }
    }
}
