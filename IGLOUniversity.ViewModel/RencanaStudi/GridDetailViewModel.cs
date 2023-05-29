using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.RencanaStudi
{
    public class GridDetailViewModel
    {
        public int Id { get; set; }

        public string Nama { get; set; }
        public int IdMatakuliah { get; set; }
        public string Matakuliah { get; set; }
        public int SKS { get; set; }
        public string Semester { get; set; }
        public int Kapasitas { get; set; }
        public string Status { get; set; }
    }
}
