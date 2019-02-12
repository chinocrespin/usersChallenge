using System;
using System.Collections.Generic;
using Core.EF;
using Identity.Domain.IRepositories;
using Identity.Domain.Models;

namespace Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(IUnitOfWork context) : base(context)
        {
        }

        public bool Add(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User GetById(string idValue)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
