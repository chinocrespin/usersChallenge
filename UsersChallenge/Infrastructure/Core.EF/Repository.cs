using Microsoft.EntityFrameworkCore;

namespace Core.EF
{
    public abstract class Repository<T> where T : class
    {
        private readonly IUnitOfWork _context;

        protected Repository(IUnitOfWork context)
        {
            _context = context;
        }
        
        protected DbSet<T> DbSet => _context.GetDbContext().Set<T>();
        protected DbContext Context => _context.GetDbContext();

        protected int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
