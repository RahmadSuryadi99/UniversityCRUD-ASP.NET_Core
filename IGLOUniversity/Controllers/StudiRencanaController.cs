using IgloUniversity.Provider;
using IGLOUniversity.ViewModel.RencanaStudi;
using Microsoft.AspNetCore.Mvc;

namespace IGLOUniversity.Controllers
{
    public class StudiRencanaController : BaseController
    {
        public IActionResult Index()
        {
            SetUsernameRole(User.Claims);
            var model = RencanaStudiProvider.GetProvider().GetIndex();
            return View(model);
        }
        public IActionResult Detail(int id )
        {
            SetUsernameRole(User.Claims);
            var model = RencanaStudiProvider.GetProvider().GetDetail(id);
            return View(model);
        }
    }
}
