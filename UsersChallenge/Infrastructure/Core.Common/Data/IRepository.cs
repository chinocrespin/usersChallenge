using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Common.Data
{
    public interface IRepository<T> where T : Entity
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T GetById(string idValue);
        IQueryable<T> GetAll();
    }
}
