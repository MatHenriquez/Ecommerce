using Ecommerce.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void BulkRemove(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public async Task<T?> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, bool istracking = true)
        {
            IQueryable<T> query = dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            if(!istracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetFirst(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool istracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (!istracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
