﻿using IGLOUniversity.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.ViewModel.User
{
    public class GridUserViewModel
    {
        public int Id { get; set; }
           public string UserName { get; set; }
        public string Status { get; set; }
        public bool IsAdmin { get; set; }
        public int? MahasiswaId { get; set; }
        public string Password { get; set; }
        public string Mahasiswa { get; set; }
    }
}
