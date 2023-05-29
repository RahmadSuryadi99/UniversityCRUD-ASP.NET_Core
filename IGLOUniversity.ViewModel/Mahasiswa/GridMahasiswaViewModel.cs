using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Mahasiswa
{
    public class GridMahasiswaViewModel
    {
        [Required(ErrorMessage ="tidak boleh kosong")]
        public int Id { get; set; }
        [Required(ErrorMessage ="tidak boleh kosong")]
        public string Nim { get; set; }
        [Required(ErrorMessage ="tidak boleh kosong")]
        public string NamaDepan { get; set; }
        public string NamaTengah { get; set; }
        public string NamaBelakang { get; set; }
        public string AsalSma { get; set; }
        public string NomorHp { get; set; }
        public string Alamat { get; set; }
    }
}
