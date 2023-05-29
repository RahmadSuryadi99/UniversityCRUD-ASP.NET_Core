using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.RencanaStudi
{
    public class DetailRencanaStudiViewModel
    {
        public string NIM { get; set; }
        public string Nama { get; set; }
        public string AsalSma { get; set; }
        public List<GridDetailViewModel> Grid { get; set; }
    }
}
