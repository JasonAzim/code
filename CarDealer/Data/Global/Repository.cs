using System;
using System.Linq;

namespace Global.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public int Create(T entity)
        {
            var obj = (T)Activator.CreateInstance(entity.GetType());
            return 0;
        }

        public void Delete(T entity)
        {
            //Write your logic here to delete an entity

        }

        public T GetById(int id)
        {
            Type typeParameterType = typeof(T);
            
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            //Write your logic here to update an entity

        }

    }
}
