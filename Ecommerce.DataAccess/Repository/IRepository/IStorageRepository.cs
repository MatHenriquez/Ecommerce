using Ecommerce.Models;

namespace Ecommerce.DataAccess.Repository.IRepository
{
    public interface IStorageRepository : IRepository<Storage>
    {
        void Update(Storage storage);
    }
}
