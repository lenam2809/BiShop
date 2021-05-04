using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.ViewModel
{
    public class HoaDon
    {
        public int Id { get; set; }

        public KHACHHANG khachHang { get; set; }

        public List<SanPham> sanPhams { get; set; }
    }
}