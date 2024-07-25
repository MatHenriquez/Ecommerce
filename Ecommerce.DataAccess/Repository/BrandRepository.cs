using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;

namespace Ecommerce.DataAccess.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _db;

        public BrandRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Brand brand)
        {
            var currentBrand = _db.Categories.Find(brand.Id);
            if (currentBrand != null)
            {
                currentBrand.Name = brand.Name;
                currentBrand.Description = brand.Description;
                currentBrand.Status = brand.Status;

                _db.SaveChanges();
            }
        }
    }
}
