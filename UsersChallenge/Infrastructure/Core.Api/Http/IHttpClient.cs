using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Api.Models;

namespace Core.Api.Http
{
    public interface IHttpClient
    {
        HttpClient Client { get; }
        Task<TRespuesta> PostAsync<TEntrada, TRespuesta>(string accion, TEntrada tEntrada, AuthenticationHeader authHeader = null);
        Task<TRespuesta> GetAsync<TRespuesta>(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null);
        Task<string> GetPlainAsync(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null);
        Task<TRespuesta> DeleteAsync<TRespuesta>(string accion, IDictionary<string, string> parameters = null, AuthenticationHeader authHeader = null);
        void CancelPendingRequests();
    }
}
