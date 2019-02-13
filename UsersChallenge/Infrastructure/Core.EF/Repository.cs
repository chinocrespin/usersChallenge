using System.Linq;
using Core.Common.Data;
using Microsoft.EntityFrameworkCore;

namespace Core.EF
{
    public abstract class Repository<T> where T : Entity
    {
        private readonly IUnitOfWork _context;

        protected Repository(IUnitOfWork context)
        {
            _context = context;
        }
        
        protected DbSet<T> DbSet => _context.GetDbContext().Set<T>();

        public virtual bool Create(T entity)
        {
            DbSet.Add(entity);
            return SaveChanges() > 0;
        }

        public virtual bool Update(T entity)
        {
            DbSet.Update(entity);
            return SaveChanges() > 0;
        }

        public virtual bool Delete(T entity)
        {
            DbSet.Remove(entity);
            return SaveChanges() > 0;
        }

        public virtual T GetById(string id)
        {
            return DbSet.FirstOrDefault(x => x.IdValue == id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        protected int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
