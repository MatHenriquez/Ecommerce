using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;

namespace Ecommerce.DataAccess.Repository
{
    public class StorageRepository : Repository<Storage>, IStorageRepository
    {
        private readonly ApplicationDbContext _db;

        public StorageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Storage storage)
        {
            var currentStorage = _db.Storages.Find(storage.Id);
            if (currentStorage != null)
            {
                currentStorage.Name = storage.Name;
                currentStorage.Description = storage.Description;
                currentStorage.Status = storage.Status;

                _db.SaveChanges();
            }
        }
    }
}
