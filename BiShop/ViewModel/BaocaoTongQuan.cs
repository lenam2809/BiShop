using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.ViewModel
{
    public class BaocaoTongQuan
    {
        private DbBiLuxuryEntities data = new DbBiLuxuryEntities();

        public int spNoiBatId { 
            get
            {
                return data.CTDonHangs.GroupBy(x => x.MaSP).Select(s => new { Id = s.Key, tongSL = s.Sum(x => x.SoLuong) }).OrderByDescending(x => x.tongSL).Take(1).Select(x=>x.Id).FirstOrDefault();
            }
        }

        public SanPham sanPhamNoiBat
        {
            get
            {
                return data.SanPhams.FirstOrDefault(x => x.MaSP == spNoiBatId);
            }
        }

        public int? SLsanPhamNoiBat
        {
            get
            {
                return data.CTDonHangs.GroupBy(x => x.MaSP).Select(s => new { Id = s.Key, tongSL = s.Sum(x => x.SoLuong) }).OrderByDescending(x => x.tongSL).Take(1).Select(x => x.tongSL).FirstOrDefault();
            }
        }

        public int PhanTramDoanhThu
        {
            get
            {
                return 100 ;
            }
        }



    }
}