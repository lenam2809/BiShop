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
    public class CTDonHangsController : Controller
    {
        private DbBiLuxuryEntities db = new DbBiLuxuryEntities();

        // GET: Admin/CTDonHangs
        public async Task<ActionResult> Index()
        {
            var cTDonHangs = db.CTDonHangs.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(await cTDonHangs.ToListAsync());
        }

        // GET: Admin/CTDonHangs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDonHang cTDonHang = await db.CTDonHangs.FindAsync(id);
            if (cTDonHang == null)
            {
                return HttpNotFound();
            }
            return View(cTDonHang);
        }

        // GET: Admin/CTDonHangs/Edit/5
        public async Task<ActionResult> Edit(int? id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDonHang cTDonHang = await db.CTDonHangs.FirstOrDefaultAsync(x=>x.MaDH==id1 && x.MaSP==id2);
            if (cTDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaKH", cTDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTDonHang.MaSP);
            return View(cTDonHang);
        }

        // POST: Admin/CTDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaDH,MaSP,SoLuong,GhiChu")] CTDonHang cTDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTDonHang).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "DonHangs", new { id = cTDonHang.MaDH });
            }
            ViewBag.MaDH = new SelectList(db.DonHangs, "MaDH", "MaKH", cTDonHang.MaDH);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cTDonHang.MaSP);
            return View(cTDonHang);
        }

        // GET: Admin/CTDonHangs/Delete/5
        public async Task<ActionResult> Delete(int? id1, int? id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDonHang cTDonHang = await db.CTDonHangs.FirstOrDefaultAsync(x => x.MaDH == id1 && x.MaSP == id2);
            if (cTDonHang == null)
            {
                return HttpNotFound();
            }
            return View(cTDonHang);
        }

        // POST: Admin/CTDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id1, int? id2)
        {
            CTDonHang cTDonHang = await db.CTDonHangs.FirstOrDefaultAsync(x => x.MaDH == id1 && x.MaSP == id2);
            db.CTDonHangs.Remove(cTDonHang);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "DonHangs", new { id = id1 });
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
