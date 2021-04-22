using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BiShop.Models;

namespace BiShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReviewsController : Controller
    {
        private DbBiLuxuryEntities db = new DbBiLuxuryEntities();

        // GET: Admin/Reviews
        public async Task<ActionResult> Index()
        {
            var reviews = db.Reviews.Include(r => r.KHACHHANG).Include(r => r.SanPham);
            return View(await reviews.ToListAsync());
        }


        // GET: Admin/Reviews/Delete/5
        public async Task<ActionResult> Delete(int? id1, int? id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = await db.Reviews.FirstOrDefaultAsync(x=>x.MaKH==id1 && x.MaSP==id2);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Admin/Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id1, int id2)
        {
            Review review = await db.Reviews.FirstOrDefaultAsync(x => x.MaSP == id2 && x.MaKH == id1);
            db.Reviews.Remove(review);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
