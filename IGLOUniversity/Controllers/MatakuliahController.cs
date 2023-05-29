using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Provider;
using IGLOUniversity.Repository;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.Matakuliah;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IGLOUniversity.Controllers
{

    [Authorize(Roles ="Admin")]
    public class MatakuliahController : BaseController
    {

        public IActionResult Index(int page = 1)
        {
            SetUsernameRole(User.Claims);
            var model = MatakuliahProvider.GetProvider().GetIndex(page);
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] UpsertMatakuliahViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MatakuliahProvider.GetProvider().Save(model);
                    return Json(new { success = true });

                }
                catch
                {
                    return Json(new { success = false });
                }
            }
            else
            {

                var valMsg = GetValidationViewModels(ModelState);
                return Json(new { success = false, validation = valMsg });
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {

                var model = MatakuliahProvider.GetProvider().GetEdit(id);
                return Json(model);
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(int id)
        {
            if (MatakuliahProvider.GetProvider().Delete(id))
            {
                return Json(new { success = true, message = "berhasil terhapus" });

            }
            return Json(new { success = false, message = "gagal terhapus" });
        }

        [HttpGet]
        public IActionResult GetDropDownMatakuliah()
        {
            var data = MatakuliahProvider.GetProvider().GetDataMatakuliah();
            return Json(data);
        }
    }
}
