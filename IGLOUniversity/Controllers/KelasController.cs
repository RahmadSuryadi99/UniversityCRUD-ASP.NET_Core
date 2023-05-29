using IGLOUniversity.DataAccess.Models;
using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Kelas;
using IGLOUniversity.ViewModel.Mahasiswa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IGLOUniversity.Controllers
{

    [Authorize(Roles = "Admin")]

    public class KelasController : BaseController
    {
        public IActionResult Index()
        {

            SetUsernameRole(User.Claims);
            var model = KelasProvider.GetProvider().GetIndex();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] UpsertKelasViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {

                        if (KelasProvider.GetProvider().Save(model))
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
                        if (KelasProvider.GetProvider().PostEditKela(model))
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
            var data = KelasProvider.GetProvider().GetEditKela(id);
            return Json(data);
        }
        [AcceptVerbs("POST", "GET")]
        public IActionResult Delete(int id)
        {
            var result = KelasProvider.GetProvider().Delete(id);
            return Json(new { success = result });
        }
    }
}
