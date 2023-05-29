using System;
using System.Collections.Generic;

#nullable disable

namespace IGLOUniversity.DataAccess.Models
{
    public partial class Mahasiswa
    {
        public Mahasiswa()
        {
            DistribusiMatakuliahs = new HashSet<DistribusiMatakuliah>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Nim { get; set; }
        public string NamaDepan { get; set; }
        public string NamaTengah { get; set; }
        public string NamaBelakang { get; set; }
        public string AsalSma { get; set; }
        public string NomorHp { get; set; }
        public string Alamat { get; set; }

        public virtual ICollection<DistribusiMatakuliah> DistribusiMatakuliahs { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
