using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BiShop.Controllers
{
    public class ProductController : Controller
    {
        DbBiLuxuryEntities db = new DbBiLuxuryEntities();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DetailProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = await db.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        public ActionResult ListProduct(int id)
        {
            var dsSanPham = db.SanPhams.Where(x => x.MaLSP == id).ToList();
            ViewBag.dsSanPham = dsSanPham;
            return View();
        }
    }
}