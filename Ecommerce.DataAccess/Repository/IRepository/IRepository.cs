using System.Linq.Expressions;

namespace Ecommerce.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null,
            bool istracking = true
            );
        Task<T?> GetFirst(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null,
            bool? istracking = true
            );
        Task Add(T entity);
        void Remove(T entity);
        void BulkRemove(IEnumerable<T> entities);
    }
}
