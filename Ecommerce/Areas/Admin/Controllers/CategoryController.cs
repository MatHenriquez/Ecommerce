using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Category? category = new ();
            if (id == null)
            {
                category.Status = true;
                return View(category);
            }

            category = await _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = await _unitOfWork.Category.GetAll();
            return Json(new { data = allCategories });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    await _unitOfWork.Category.Add(category);
                    TempData[StaticDefinitions.Success] = "Category added successfully";
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                    TempData[StaticDefinitions.Success] = "Category updated successfully";
                }
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[StaticDefinitions.Error] = "Error";
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Category.Get(id);
            if (category == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Category.Remove(category);
            await _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

        [ActionName("ValidateName")]
        public async Task<IActionResult> ValidateName(string name, int id = 0)
        {
            bool value = false;
            var categories = await _unitOfWork.Category.GetAll();

            if(id == 0)
            {
                value = categories.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            }
            else
            {
                value = categories.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim() && x.Id != id);
            }

            if(value)
            {
                return Json(new { data = true });
            }

            return Json(new { data = false });
        }
        #endregion
    }
}
