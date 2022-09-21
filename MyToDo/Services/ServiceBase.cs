using MyToDo.Shared;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using MyToDo.Shared.Contact;

namespace MyToDo.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly HttpRestClient _client;
        private readonly string _serviceName;

        public ServiceBase(HttpRestClient client, string serviceName)
        {
            _client = client;
            _serviceName = serviceName;
        }

        public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            RequestBase request = new RequestBase();
            request.Method = RestSharp.Method.POST;
            request.Route = @$"api/{_serviceName}/Add";
            request.Parameter = entity;
            return await _client.ExcuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            RequestBase request = new RequestBase();
            request.Method = RestSharp.Method.DELETE;
            request.Route = @$"api/{_serviceName}/Delete?id={id}";
            return await _client.ExcuteAsync(request);
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameters parameters)
        {
            RequestBase request = new RequestBase();
            request.Method = RestSharp.Method.GET;
            request.Route = $"api/{_serviceName}/GetAll?PageIndex={parameters.PageIndex}" +
                            $"&PageSize={parameters.PageSize}" +
                            $"&Searchstring={parameters.SearchString}";
            return await _client.ExcuteAsync<PagedList<TEntity>>(request);
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            RequestBase request = new RequestBase();
            request.Method = RestSharp.Method.GET;
            request.Route = @$"api/{_serviceName}/Get?id={id}";
            return await _client.ExcuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            RequestBase request = new RequestBase();
            request.Method = RestSharp.Method.POST;
            request.Route = @$"api/{_serviceName}/Update";
            request.Parameter = entity;
            return await _client.ExcuteAsync<TEntity>(request);
        }
    }
}