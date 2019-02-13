using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Domain.Models;

namespace Identity.Domain.Http
{
    public interface IRandomUsersHttpClient
    {
        Task<IEnumerable<User>> GetRandomUsers();
    }
}
