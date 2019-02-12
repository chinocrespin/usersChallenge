using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.Data
{
    public interface IRepository<T> where T : Entity
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T GetById(string idValue);
        ICollection<T> GetAll();
    }
}
