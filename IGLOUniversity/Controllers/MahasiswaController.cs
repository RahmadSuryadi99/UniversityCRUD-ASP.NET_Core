
using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGLOUniversity.Controllers
{

    [Authorize(Roles = "Admin")]
    public class MahasiswaController : BaseController
    {
        public IActionResult Index(int page = 1)
        {

            SetUsernameRole(User.Claims);
            var model = MahasiswaProvider.GetProvider().GetIndex(page);
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] GridMahasiswaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {

                        if (MahasiswaProvider.GetProvider().Save(model))
                        {
                            return Json(new { success = true, message = "Inssert berhasil" });
                        }
                        else
                        {
                            return Json(new { success = false });

                        }
                    }
                    else
                    {
                        if (MahasiswaProvider.GetProvider().PostEditMahasiswa(model))
                        {
                            return Json(new { success = true, message = "Update berhasil" });
                        }
                        else
                        {
                            return Json(new { success = false });
                        }
                        
                    }
                }

                var valMsg = GetValidationViewModels(ModelState);
                return Json(new { success = false, validation = valMsg });
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {

                var model = MahasiswaProvider.GetProvider().GetEditMahasiswa(id);
                return Json(model);
            }
            catch
            {
                return Json(new { success = false });
            }
        }
        [AcceptVerbs("POST","GET")]
        public IActionResult Delete(int id)
        {
            var result=MahasiswaProvider.GetProvider().Delete(id);
            return Json(new { success = result });
        }
    }
}
