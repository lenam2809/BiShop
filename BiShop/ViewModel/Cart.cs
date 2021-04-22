using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.Models
{
    [Serializable]
    public class Cart
    {
        public SanPham SanPham { get; set; }

        public int SoLuong { get; set; }

    }
}