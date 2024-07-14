using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StorageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StorageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Storage? storage = new ();
            if (id == null)
            {
                storage.Status = true;
                return View(storage);
            }

            storage = await _unitOfWork.Storage.Get(id.GetValueOrDefault());
            if (storage == null)
            {
                return NotFound();
            }
            return View(storage);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allStorages = await _unitOfWork.Storage.GetAll();
            return Json(new { data = allStorages });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Storage storage)
        {
            if (ModelState.IsValid)
            {
                if (storage.Id == 0)
                {
                    await _unitOfWork.Storage.Add(storage);
                }
                else
                {
                    _unitOfWork.Storage.Update(storage);
                }
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(storage);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var storage = await _unitOfWork.Storage.Get(id);
            if (storage == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Storage.Remove(storage);
            await _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
