using IGLOUniversity.Provider;
using IGLOUniversity.ViewModel.Mahasiswa;
using IGLOUniversity.ViewModel.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IGLOUniversity.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index(int page = 1)
        {
            SetUsernameRole(User.Claims);
            var model = UserProvider.GetProvider().GetIndex(page);
            return View(model);
        }

        public IActionResult GetStatusDropDown()
        {
            var dataDropDown = UserProvider.GetProvider().GetDataStatus();
            return Json(dataDropDown);
        }
        public IActionResult GetMahasiswaDropDown()
        {
            var dataDropDown = UserProvider.GetProvider().GetDataMahasiswa();
            return Json(dataDropDown);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Save([FromBody] UpsertUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = UserProvider.GetProvider().Save(model);
                    return Json( new {success=result});
                }
                catch (Exception ex)
                {
                    //return null;
                }
            }
            var valMsg = GetValidationViewModels(ModelState);
           return Json(new { success = false, validation = valMsg });
        }

        [HttpGet]
        public IActionResult EditPopup(int id)
        {
            var model = UserProvider.GetProvider().GetEdit(id);
            return Json(model);
        }

        [AcceptVerbs("POST", "GET")]
        public IActionResult DeletePopup(int id)
        {
            if (!UserProvider.GetProvider().Delete(id))
            {
                return Json(new { success = false, message = "You can't delete this category" });
            }
            else
            {
                return Json(new { success = true, message = "Your record has been deleted" });
            }
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var modal = new LoginViewModel() { };
            return View(modal);
        }
        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, LoginViewModel model)
        {
            if (ModelState.IsValid)
                {
                if (UserProvider.GetProvider().IsAuthentication(model))
                {
                    var claims = new List<Claim>()
                    {
                        new Claim("username", model.Username),
                        new Claim(ClaimTypes.NameIdentifier, model.Username),
                        new Claim(ClaimTypes.Role, UserProvider.GetProvider().GetRoleName(model.Username))
                    };
    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);

                    if (returnUrl == null)
                    {
                        if (UserProvider.GetProvider().GetRoleName(model.Username) == "Mahasiswa")
                        {
                            return RedirectToAction("Index", "Home", new { model.Username });
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    //gagal authentication\

                    return RedirectToAction("LoginFailed" );
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult LoginFailed(string? returnUrl)
        {

            ViewBag.halaman = returnUrl;
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
