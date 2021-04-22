using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.Models
{
    public class LSThanhToan
    {
        public int MaKH { get; set; }

        public int MaSP { get; set; }

        public string TenSP { get; set; }

        public int SoLuong { get; set; }

        public int Gia { get; set; }

        public DateTime NgayDH { get; set; }

        public string TrangThai { get; set; }
    }
}