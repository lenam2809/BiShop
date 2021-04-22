using BiShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiShop.Controllers
{
    public class ShopController : Controller
    {
        DbBiLuxuryEntities db = new DbBiLuxuryEntities();
        // GET: Shop
        public ActionResult Index(int? id, int? gia, int? page, int pageSize = 12)
        {
            if (page == null) page = 1;
            var sanpham = db.SanPhams.ToList();
            if(id != null)
            {
                sanpham = db.SanPhams.Where(x => x.MaLSP == id).ToList();
            }    
            int pageNumber = (page ?? 1);

            var lsp = db.LoaiSanPhams;
            ViewBag.lsp = lsp;

            var ncc = db.NCCs;
            ViewBag.ncc = ncc;

            return View(sanpham.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ShopDetail(int? id)
        {
            var detail = db.SanPhams.FirstOrDefault(x => x.MaSP == id);

            var img = db.AnhSPs.FirstOrDefault(x => x.LinkId == detail.LinkAnh);
            ViewBag.img = img;



            var category = db.LoaiSanPhams.FirstOrDefault(x => x.Id == detail.MaLSP);
            ViewBag.category = category;
            return View(detail);
        }
    }
}