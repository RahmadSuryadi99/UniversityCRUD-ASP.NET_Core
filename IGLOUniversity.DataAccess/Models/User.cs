using System;
using System.Collections.Generic;

#nullable disable

namespace IGLOUniversity.DataAccess.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public bool IsAdmin { get; set; }
        public int? MahasiswaId { get; set; }
        public string Password { get; set; }

        public virtual Mahasiswa Mahasiswa { get; set; }
    }
}
