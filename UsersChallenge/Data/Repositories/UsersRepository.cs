using System.Linq;
using Core.EF;
using Identity.Domain.IRepositories;
using Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(IUnitOfWork context) : base(context)
        {
        }

        public override IQueryable<User> GetAll()
        {
            return DbSet.Include(x => x.Location);
        }
    }
}
