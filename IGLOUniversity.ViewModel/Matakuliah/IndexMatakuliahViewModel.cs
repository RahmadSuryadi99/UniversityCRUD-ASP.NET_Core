using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.Matakuliah
{
    public class IndexMatakuliahViewModel 
    {

        public int TotalData { get; set; }
        public int TotalHalaman { get; set; }
        public List<GridMatakuliahViewModel> Grid { get; set; }
    }
}
