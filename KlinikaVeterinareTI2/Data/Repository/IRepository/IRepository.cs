using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KlinikaVeterinareTI2.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null, // me filtru rezultatin qe te vjen
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, // order by dicka
            string includeProperties = null
            );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );

        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);
    }
}
