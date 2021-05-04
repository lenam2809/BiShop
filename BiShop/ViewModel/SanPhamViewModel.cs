using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.ViewModel
{
    [Serializable]
    public class SanPhamViewModel
    {
        public SanPham sanPham { get; set; }

        public int Discount 
        {
            get
            {
                if(sanPham.LoaiSanPham.KhuyenMai.Discount == null)
                {
                    return (int)sanPham.Gia;
                } else
                {
                    return (int)(sanPham.Gia - (sanPham.LoaiSanPham.KhuyenMai.Discount * sanPham.Gia / 100));
                }
            }
        }
    }
}