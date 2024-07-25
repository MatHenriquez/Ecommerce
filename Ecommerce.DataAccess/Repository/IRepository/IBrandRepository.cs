using Ecommerce.Models;

namespace Ecommerce.DataAccess.Repository.IRepository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        void Update(Brand brand);
    }
}
