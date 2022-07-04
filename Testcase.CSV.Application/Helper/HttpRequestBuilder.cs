using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcase.CSV.Domain.Responses;

namespace Testcase.CSV.Application.Helper
{
    public class HttpRequestBuilder
    {
        private RestRequest request;
        private RestClient client;

        public HttpRequestBuilder(RestClient client)
        {
            this.client = client;
        }
        public HttpRequestBuilder AddAuthorization(string token)
        {
            this.request.AddHeader("Authorization", $"Bearer {token}");
            this.request.AddHeader("Content-Type", "application/json");
            return this;
        }
        public static HttpRequestBuilder GetInstance(string baseUrl)
        {
            RestClient clientObject = new RestClient(baseUrl);
            return new HttpRequestBuilder(clientObject);
        }
        public HttpRequestBuilder AddHeader(string key, string value)
        {
            this.request.AddHeader(key, value);
            return this;
        }

        public HttpRequestBuilder AddFormUrlParameter(Dictionary<string, string> formUrlParameter)
        {
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            foreach (var item in formUrlParameter)
                this.request.AddParameter(item.Key, item.Value);
            return this;
        }

        public HttpRequestBuilder AddJsonBody(string body)
        {

            this.request.AddParameter("application/json", body, ParameterType.RequestBody);
            return this;
        }

        public HttpRequestBuilder Delete()
        {
            this.request = new RestRequest(Method.DELETE);
            return this;

        }

        public HttpRequestBuilder Get()
        {
            this.request = new RestRequest(Method.GET);
            return this;
        }

        public HttpRequestBuilder Post()
        {
            this.request = new RestRequest(Method.POST);
            return this;
        }

        public HttpRequestBuilder Put()
        {
            this.request = new RestRequest(Method.PUT);
            return this;
        }

        public async Task<APIResponse<T>> SendAsync<T>()
        {
            APIResponse<T> result = new APIResponse<T>();
            var response = await client.ExecuteAsync(this.request);
            if (response.IsSuccessful)
                result.SetData(JsonConvert.DeserializeObject<T>(response.Content));
            else
                result.SetFailure($"Status Code: {response.StatusCode.ToString()}. Hata Mesajı: {response.Content}");
            return result;
        }

    }
}
