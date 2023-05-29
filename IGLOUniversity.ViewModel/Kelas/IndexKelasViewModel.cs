using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Kelas
{
    public class IndexKelasViewModel
    {
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public List<GridKelasViewModel> Grid { get; set;}
    }
}
