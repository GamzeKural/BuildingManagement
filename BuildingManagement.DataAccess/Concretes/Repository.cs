using BuildingManagement.DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BuildingManagement.DataAccess.Concretes
{
    public class Repository : IRepository
    {
        private DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add<T>(T entity) where T : class
        {
            _dbContext.Add(entity);
        }

        public T Get<T>(int Id) where T : class, new()
        {
            return _dbContext.Set<T>().Find(Id);
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _dbContext.Set<T>().Where(predicate).AsNoTracking();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Detached;

            var model = _dbContext.Update<T>(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
