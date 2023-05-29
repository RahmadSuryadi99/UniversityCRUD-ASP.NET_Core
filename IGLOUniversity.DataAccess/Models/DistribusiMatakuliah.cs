using System;
using System.Collections.Generic;

#nullable disable

namespace IGLOUniversity.DataAccess.Models
{
    public partial class DistribusiMatakuliah
    {
        public int Id { get; set; }
        public int IdMahasiswa { get; set; }
        public int IdKelas { get; set; }
        public string Status { get; set; }

        public virtual Kela IdKelasNavigation { get; set; }
        public virtual Mahasiswa IdMahasiswaNavigation { get; set; }
    }
}
