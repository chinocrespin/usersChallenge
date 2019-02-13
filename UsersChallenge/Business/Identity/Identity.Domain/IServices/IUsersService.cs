using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Common.Data;
using Core.Common.Presentation;
using Identity.Domain.Models;
using Identity.Domain.Queries;

namespace Identity.Domain.IServices
{
    public interface IUsersService : IService
    {
        User GetById(string id);
        bool Update(User user);
        bool Delete(string id);
        Result<UserResult> GetAll(UsersQuery query);
        Task<IEnumerable<User>> GetRandomUsers();
    }
}
