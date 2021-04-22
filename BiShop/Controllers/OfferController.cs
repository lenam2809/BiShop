using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiShop.Controllers
{
    public class OfferController : Controller
    {
        DbBiLuxuryEntities db = new DbBiLuxuryEntities();
        // GET: Offer
        public ActionResult Index()
        {
            //Danh mục sản phẩm
            var lsp = db.LoaiSanPhams.ToList();
            ViewBag.lsp = lsp;

            //áo nam
            var aonam = db.SanPhams.Where(x => x.MaLSP == 3).ToList();
            ViewBag.aonam = aonam;

            //quần nam
            var quannam = db.SanPhams.Where(x => x.MaLSP == 5).ToList();
            ViewBag.quannam = quannam;

            //phụ kiện nam
            var phukiennam = db.SanPhams.Where(x => x.MaLSP == 2).ToList();
            ViewBag.phukiennam = phukiennam;

            //đồ lót nam
            var dolotnam = db.SanPhams.Where(x => x.MaLSP == 6).ToList();
            ViewBag.dolotnam = dolotnam;

            //vest
            var vest = db.SanPhams.Where(x => x.MaLSP == 1).ToList();
            ViewBag.vest = vest;

            //quần áo thu đông
            var quanaothudong = db.SanPhams.Where(x => x.MaLSP == 7).ToList();
            ViewBag.quanaothudong = quanaothudong;

            return View();
        }
    }
}