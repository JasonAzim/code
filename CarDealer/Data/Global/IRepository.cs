using System;
using System.Linq;
using System.Collections.Generic;

namespace Global.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(int id);

        int Create(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
