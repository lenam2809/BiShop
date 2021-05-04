using BiShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.Models
{
    [Serializable]
    public class Cart
    {
        public SanPhamViewModel SanPhamViewModel { get; set; }

        public int SoLuong { get; set; }

        public int TongTien
        {
            get
            {
                return SoLuong * SanPhamViewModel.Discount;
            }
        }

    }
}