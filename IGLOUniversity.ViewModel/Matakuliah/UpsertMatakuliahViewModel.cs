using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Matakuliah
{
    public class UpsertMatakuliahViewModel
    {
        [Required(ErrorMessage ="tidak boleh kosong")]
        public int Id { get; set; }
        [Required(ErrorMessage ="tidak boleh kosong")]
        public string Nama { get; set; }
        [Required(ErrorMessage ="tidak boleh kosong")]
        public int Sks { get; set; }
        public string Deskripsi { get; set; }
    }
}
