using Ecommerce.DataAccess.Repository.IRepository;
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

        #region API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allStorages = await _unitOfWork.Storage.GetAll();
            return Json(new { data = allStorages });
        }
        #endregion
    }
}
