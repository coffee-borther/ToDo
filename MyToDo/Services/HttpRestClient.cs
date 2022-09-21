using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyToDo.Shared;
using Newtonsoft.Json;
using RestSharp;

namespace MyToDo.Services
{
    public class HttpRestClient
    {
        private readonly string _apiUrl;
        protected readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            _apiUrl = apiUrl;
            client = new RestClient();
        }

        public async Task<ApiResponse> ExcuteAsync(RequestBase requestBase)
        {
            var request = new RestRequest(requestBase.Method);
            request.AddHeader("Content-Type", requestBase.ContentType);
            if (requestBase.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(requestBase.Parameter),
                    ParameterType.RequestBody);
            client.BaseUrl = new Uri(_apiUrl + requestBase.Route);
            var response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
        }

        public async Task<ApiResponse<T>> ExcuteAsync<T>(RequestBase requestBase)
        {
            var request = new RestRequest(requestBase.Method);
            request.AddHeader("Content-Type", requestBase.ContentType);
            if (requestBase.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(requestBase.Parameter),
                    ParameterType.RequestBody);
            client.BaseUrl = new Uri(_apiUrl + requestBase.Route);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            }
            else
            {
                return new ApiResponse<T>()
                {
                    Status = false,
                    Message = response.ErrorMessage
                };
            }
        }
    }
}
