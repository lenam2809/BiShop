using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.ViewModel
{
    [Serializable]
    public class BaoCao
    {
        public CTDonHang ctDonHang { get; set; }

        public string NCC 
        {
            get
            {
                return ctDonHang.SanPham.NCC.TenNCC;
            }
        }

        public string LSP
        {
            get
            {
                return ctDonHang.SanPham.LoaiSanPham.TenLoai;
            }
        }

        public string TenSP
        {
            get
            {
                return ctDonHang.SanPham.TenSP;
            }
        }

        public int SoLuongThuc
        {
            get
            {
                return (int)ctDonHang.SoLuong;
            }
        }

        public int DoanhThu 
        {
            get
            {
                return (int)(ctDonHang.SoLuong*ctDonHang.SanPham.Gia);
            }
        }

        public int GiamGia 
        { 
            get
            {
                return (int)(ctDonHang.SanPham.LoaiSanPham.KhuyenMai.Discount * ctDonHang.SanPham.Gia / 100 * ctDonHang.SoLuong);
            }
        }   

        public int DoanhThuThuc 
        { 
            get
            {
                return (DoanhThu - GiamGia);
            }
        }

        public int TongDoanhThu 
        { 
            get 
            {
                return DoanhThuThuc;
            } 
        }
    }
}