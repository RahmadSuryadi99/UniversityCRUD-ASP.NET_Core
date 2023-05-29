using IGLOUniversity.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Validation
{
    internal class UniqueUsernameAttribute : ValidationAttribute
    {
        public int Length { get; set; }
        public string PropertiName { get; set; }
        public UniqueUsernameAttribute() { }
        public UniqueUsernameAttribute(int length)
        {
            this.Length = length;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                using (var context = new IGLOUniversityContext())
                {
                    var id = (int)validationContext.ObjectInstance.GetType().GetProperty(this.PropertiName).GetValue(validationContext.ObjectInstance);
                    var cek = context.Users.Any(a => a.UserName == value.ToString() && a.Id != id);
                    if (cek)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
