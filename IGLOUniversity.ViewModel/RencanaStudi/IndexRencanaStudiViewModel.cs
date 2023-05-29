using IGLOUniversity.ViewModel.Matakuliah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.RencanaStudi
{
    public class IndexRencanaStudiViewModel
    {
        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public List<GridRencanaStudiVIewMode> Grid { get; set; }
        
    }
}
