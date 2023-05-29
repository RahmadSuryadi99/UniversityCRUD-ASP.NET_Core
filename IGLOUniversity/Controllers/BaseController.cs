using Basilisk.ViewModel;
using IGLOGlobalConnfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace IGLOUniversity.Controllers
{
    public class BaseController : Controller
    {
        public void SetUsernameRole(IEnumerable<Claim> claims)
        {
            ViewBag.Username = GetUsername(claims);
            ViewBag.Role = GetRole(claims);
            SetMenuByUsername(claims);
      
        }
        protected string GetUsername(IEnumerable<Claim> claims)
        {
            return claims.SingleOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value;
        }
        protected string GetRole(IEnumerable<Claim> claims)
        {
            return claims.SingleOrDefault(a => a.Type == ClaimTypes.Role).Value;
        }
        protected void SetMenuByUsername(IEnumerable<Claim> claims)
        {
            var role = GetRole(claims);
            ViewBag.Menus = GlobalConfiguration.GetMenuByRole(role).Where(a => a.Role == role);
        }
        protected IEnumerable<ValidationViewModel> GetValidationViewModels(ModelStateDictionary modelState)
        {
            var result = new List<ValidationViewModel>();
            foreach (KeyValuePair<string, ModelStateEntry> item in modelState)
            {
                if (item.Value.Errors.Count > 0)
                {
                    var propertyName = item.Key;
                    var errorMsg = item.Value.Errors[0].ErrorMessage;
                    ///bia pake yang diatasnya atau di bawahnya
                    //var errorMsg = item.Value.Errors.FirstOrDefault().ErrorMessage;

                    result.Add(new ValidationViewModel
                    {
                        PropertyName = propertyName,
                        MessegerError = errorMsg
                    });
                }
            }
            return result;
        }
    }
}
