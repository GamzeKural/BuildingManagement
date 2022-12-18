using System;
using System.Linq;
using System.Linq.Expressions;

namespace BuildingManagement.DataAccess.Abstracts
{
    public interface IRepository
    {
        IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        T Get<T>(int Id) where T : class, new();
        T Get<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Add<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        int SaveChanges();
    }
}
