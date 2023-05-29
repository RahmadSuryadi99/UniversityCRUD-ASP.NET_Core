
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Kelas
{
    public class UpsertKelasViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nama { get; set; }
        [Required]
        public int IdMatakuliah { get; set; }
        public string Semester { get; set; }
        [Required]
        public int Kapasitas { get; set; }
        //public List<SelectListItem>? DropdownMatakuliah { get; set; }
    }
}
