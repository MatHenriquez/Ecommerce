using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;

namespace Ecommerce.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            var currentCategory = _db.Categories.Find(category.Id);
            if (currentCategory != null)
            {
                currentCategory.Name = category.Name;
                currentCategory.Description = category.Description;
                currentCategory.Status = category.Status;

                _db.SaveChanges();
            }
        }
    }
}
