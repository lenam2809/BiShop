using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.Models
{
    public class Shops
    {
        static DbBiLuxuryEntities data = new DbBiLuxuryEntities();

        public static List<SanPham> GetAll()
        {
            return data.SanPhams.ToList();
        }

        public static List<SanPham> GetSanPhamByMaLoaiSanPham(int? id)
        {
            return data.SanPhams.Where(x => x.MaLSP == id).ToList();
        }

        public static SanPham getDetaileSanPham(int? id)
        {
            return data.SanPhams.FirstOrDefault(x => x.MaSP == id);
        }

        public static List<LoaiSanPham> getLoaiSanPham()
        {
            return data.LoaiSanPhams.ToList();
        }

        public static List<NCC> getNCC()
        {
            return data.NCCs.ToList();
        }
    }
}