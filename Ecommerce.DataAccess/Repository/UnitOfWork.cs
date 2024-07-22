using Ecommerce.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IStorageRepository Storage { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Storage = new StorageRepository(_db);
            Category = new CategoryRepository(_db);
        }

        public void Dispose() => _db.Dispose();

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
