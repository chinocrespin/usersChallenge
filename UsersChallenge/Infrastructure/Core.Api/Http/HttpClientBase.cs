using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Core.Api.Models;
using Newtonsoft.Json;

namespace Core.Api.Http
{
    public class HttpClientBase : IHttpClient
    {
        protected HttpClientBase(string baseUrl)
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public HttpClient Client { get; }

        public async Task<TOutput> PostAsync<TInput, TOutput>(string action, TInput tInput, AuthenticationHeader authHeader = null)
        {
            var rawjson = JsonConvert.SerializeObject(tInput);
            Client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

            var httpResponse = await Client.PostAsync(GetActionUri(action), new StringContent(rawjson, Encoding.UTF8, "application/json"));
            var result = httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TOutput>(result?.Result);
        }

        public async Task<TOutput> GetAsync<TOutput>(string action, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            Client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

            var httpResponse = await Client.GetAsync(GetActionUri(action, parameters));
            var result = httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TOutput>(result?.Result);
        }

        public async Task<string> GetPlainAsync(string action, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            Client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

            var httpResponse = await Client.GetAsync(GetActionUri(action, parameters));
            var result = httpResponse.Content.ReadAsStringAsync();

            return result?.Result;
        }

        public async Task<TOutput> DeleteAsync<TOutput>(string action, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            Client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

            var httpResponse = await Client.DeleteAsync(GetActionUri(action, parameters));
            var result = httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TOutput>(result?.Result);
        }

        public void CancelPendingRequests()
        {
            Client.CancelPendingRequests();
        }

        private Uri GetActionUri(string action, IDictionary<string, string> parameters = null)
        {
            var builder = new UriBuilder($"{Client.BaseAddress}{action}");
            if (parameters != null && parameters.Count > 0)
            {
                var query = HttpUtility.ParseQueryString(builder.Query);
                foreach (var p in parameters)
                {
                    query[p.Key] = p.Value;
                }
                builder.Query = query.ToString();
            }
            return builder.Uri;
        }
    }
}
