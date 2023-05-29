using System;
using System.Collections.Generic;

#nullable disable

namespace IGLOUniversity.DataAccess.Models
{
    public partial class Matakuliah
    {
        public Matakuliah()
        {
            Kelas = new HashSet<Kela>();
        }

        public int Id { get; set; }
        public string Nama { get; set; }
        public int Sks { get; set; }
        public string Deskripsi { get; set; }

        public virtual ICollection<Kela> Kelas { get; set; }
    }
}
