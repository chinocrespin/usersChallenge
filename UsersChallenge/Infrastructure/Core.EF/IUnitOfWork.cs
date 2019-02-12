using System;
using Microsoft.EntityFrameworkCore;

namespace Core.EF
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext GetDbContext();
        int SaveChanges();
    }
}
