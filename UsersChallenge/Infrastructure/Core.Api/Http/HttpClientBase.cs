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

        public async Task<TRespuesta> PostAsync<TEntrada, TRespuesta>(string accion, TEntrada tEntrada, AuthenticationHeader authHeader = null)
        {
            var rawjson = JsonConvert.SerializeObject(tEntrada);
            Client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

            var httpResponse = await Client.PostAsync(GetActionUri(accion), new StringContent(rawjson, Encoding.UTF8, "application/json"));
            var result = httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TRespuesta>(result?.Result);
        }

        public async Task<TRespuesta> GetAsync<TRespuesta>(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            Client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

            var httpResponse = await Client.GetAsync(GetActionUri(accion, parameters));
            var result = httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TRespuesta>(result?.Result);
        }

        public async Task<string> GetPlainAsync(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            Client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

            var httpResponse = await Client.GetAsync(GetActionUri(accion, parameters));
            var result = httpResponse.Content.ReadAsStringAsync();

            return result?.Result;
        }

        public async Task<TRespuesta> DeleteAsync<TRespuesta>(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            Client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

            var httpResponse = await Client.DeleteAsync(GetActionUri(accion, parameters));
            var result = httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TRespuesta>(result?.Result);
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
