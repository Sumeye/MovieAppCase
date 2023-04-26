using System.Linq.Expressions;

namespace MovieAppCase.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Update(T updateEntity, T setEntity);
        void Remove(T entity);
    }
}
