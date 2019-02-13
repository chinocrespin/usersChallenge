using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomUsers.Domain.Http
{
    public interface IRandomUsersHttpClient
    {
        Task<IEnumerable<Models.RandomUser>> GetRandomUsers();
    }
}
