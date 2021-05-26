using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.ViewModel
{
    public class BaoCaoNhap
    {
        public CTNhapSanPham getCTNhapSanPham { get; set; }

        public string LSP
        {
            get
            {
                return getCTNhapSanPham.SanPham.LoaiSanPham.TenLoai;
            }
        }

        public string TenSP
        {
            get
            {
                return getCTNhapSanPham.SanPham.TenSP;
            }
        }

        public int SoLuongNhap
        {
            get
            {
                return (int)getCTNhapSanPham.SoLuong;
            }
        }

        public int GiaNhap
        {
            get
            {
                return (int)getCTNhapSanPham.GiaNhap;
            }
        }

        public DateTime NgayNhap
        {
            get
            {
                return (DateTime)getCTNhapSanPham.NhapSanPham.NgayNhap;
            }
        }

        public int ChiPhiNhap
        {
            get
            {
                return SoLuongNhap*GiaNhap;
            }
        }
    }
}