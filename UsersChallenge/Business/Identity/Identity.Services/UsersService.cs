using System.Linq;
using Core.Common.Presentation;
using Identity.Domain.IRepositories;
using Identity.Domain.IServices;
using Identity.Domain.Models;
using Identity.Domain.Queries;

namespace Identity.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User GetById(string id)
        {
            return _usersRepository.GetById(id);
        }

        public bool Update(User user)
        {
            return _usersRepository.Update(user);
        }

        public bool Delete(string id)
        {
            User user = GetById(id);
            return _usersRepository.Delete(user);
        }

        public Result<User> GetAll(UsersQuery query)
        {
            if(query == null) query = new UsersQuery();
            Result<User> result = new Result<User>(query);
            IQueryable<User> usersQuery = _usersRepository.GetAll();
            // Should keep the count of total of elements so we can know if there are more pages to consult
            result.ElementsCount = usersQuery.Count();
            result.Elements = usersQuery.OrderBy(x => x.Name)
                .Skip(query.Since)
                .Take(query.To);
            return result;
        }
    }
}
