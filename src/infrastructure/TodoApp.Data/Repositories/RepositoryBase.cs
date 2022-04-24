using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Data.Repositories
{
    internal abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly TodoDbContext DbContext;

        protected RepositoryBase(TodoDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            DbContext.Set<T>().AsNoTracking() :
            DbContext.Set<T>();

        public IQueryable<T> FindWhere(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            DbContext.Set<T>().Where(expression).AsNoTracking() :
            DbContext.Set<T>().Where(expression);

        public void Create(T entity) =>
            DbContext.Set<T>().Add(entity);

        public void Update(T entity) =>
            DbContext.Set<T>().Update(entity);

        public void Delete(T entity) =>
            DbContext.Set<T>().Remove(entity);
    }
}
