using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Api.Http;
using Identity.Domain.Models;

namespace Identity.Domain.Http
{
    public class RandomUsersHttpClient : HttpClientBase, IRandomUsersHttpClient
    {
        public RandomUsersHttpClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<IEnumerable<User>> GetRandomUsers()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("results", "500");

            return await GetAsync<IEnumerable<User>>("/api/", parameters);
        }
    }
}
