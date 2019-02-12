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
    public static class HttpClientUtil
    {
        public static async Task<TRespuesta> PostAsync<TEntrada, TRespuesta>(string accion, TEntrada tEntrada, AuthenticationHeader authHeader = null)
        {
            using (HttpClient client = new HttpClient())
            {
                var rawjson = JsonConvert.SerializeObject(tEntrada);
                client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

                var httpResponse = await client.PostAsync(GetActionUri(accion), new StringContent(rawjson, Encoding.UTF8, "application/json"));
                var result = httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TRespuesta>(result?.Result);
            }
        }

        public static async Task<TRespuesta> GetAsync<TRespuesta>(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

                var httpResponse = await client.GetAsync(GetActionUri(accion, parameters));
                var result = httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TRespuesta>(result?.Result);
            }
        }

        public static async Task<string> GetPlainAsync(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

                var httpResponse = await client.GetAsync(GetActionUri(accion, parameters));
                var result = httpResponse.Content.ReadAsStringAsync();
                return result?.Result;
            }
        }

        public static async Task<TRespuesta> DeleteAsync<TRespuesta>(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = authHeader != null ? new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Value) : null;

                var httpResponse = await client.DeleteAsync(GetActionUri(accion, parameters));
                var result = httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TRespuesta>(result?.Result);
            }
        }

        private static Uri GetActionUri(string action, IDictionary<string, string> parameters = null)
        {
            var builder = new UriBuilder(action);
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
