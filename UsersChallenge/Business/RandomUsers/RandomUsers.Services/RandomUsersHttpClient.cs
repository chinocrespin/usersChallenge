using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api.Http;
using RandomUsers.Domain.Http;
using RandomUsers.Domain.Models;
using RandomUsers.Domain.Results;

namespace RandomUsers.Services
{
    public class RandomUsersHttpClient : HttpClientBase, IRandomUsersHttpClient
    {
        public RandomUsersHttpClient(string baseUrl) : base(baseUrl)
        {
        }

        public async Task<IEnumerable<RandomUser>> GetRandomUsers()
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("results", "500");

            var randomUsers = await GetAsync<RandomUsersQueryResult>("/api/", parameters);
            return randomUsers.Results;
        }
    }
}
