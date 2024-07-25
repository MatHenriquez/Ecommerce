using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Brand? brand = new ();
            if (id == null)
            {
                brand.Status = true;
                return View(brand);
            }

            brand = await _unitOfWork.Brand.Get(id.GetValueOrDefault());
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allBrands = await _unitOfWork.Brand.GetAll();
            return Json(new { data = allBrands });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Brand brand)
        {
            if (ModelState.IsValid)
            {
                if (brand.Id == 0)
                {
                    await _unitOfWork.Brand.Add(brand);
                    TempData[StaticDefinitions.Success] = "Brand added successfully";
                }
                else
                {
                    _unitOfWork.Brand.Update(brand);
                    TempData[StaticDefinitions.Success] = "Brand updated successfully";
                }
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[StaticDefinitions.Error] = "Error";
            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.Brand.Get(id);
            if (brand == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Brand.Remove(brand);
            await _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

        [ActionName("ValidateName")]
        public async Task<IActionResult> ValidateName(string name, int id = 0)
        {
            bool value = false;
            var brand = await _unitOfWork.Brand.GetAll();

            if(id == 0)
            {
                value = brand.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            }
            else
            {
                value = brand.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim() && x.Id != id);
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
