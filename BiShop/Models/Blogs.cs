using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BiShop.Models
{
    public class Blogs
    {
        static DbBiLuxuryEntities data = new DbBiLuxuryEntities();

        public static List<TinTuc> GetAll()
        {
            return data.TinTucs.ToList();
        }

        public static List<TinTuc> GetTinTucByMaLoaiTin(int? id)
        {
            return data.TinTucs.Where(x => x.MaLoaiTin == id).ToList();
        }

        public static TinTuc tinTucDetail(int id)
        {
            return data.TinTucs.FirstOrDefault(x => x.Id == id);
        }
    }
}