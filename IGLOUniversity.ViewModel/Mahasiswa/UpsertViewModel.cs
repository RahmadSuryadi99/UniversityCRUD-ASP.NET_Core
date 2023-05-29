using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Mahasiswa
{
    public class UpsertViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public int? MahasiswaId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
