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
    public class DonHangsController : Controller
    {
        private DbBiLuxuryEntities db = new DbBiLuxuryEntities();

        // GET: Admin/DonHangs
        public async Task<ActionResult> Index()
        {
            var donHangs = db.DonHangs.Include(d => d.KHACHHANG).Include(d => d.Ship);            
            return View(await donHangs.ToListAsync());
        }

        // GET: Admin/DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ctdonHang = db.CTDonHangs.Where(x=>x.MaDH==id).ToList();
            if (ctdonHang == null)
            {
                return HttpNotFound();
            }
            return View(ctdonHang);
        }


        // GET: Admin/DonHangs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = await db.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDH = new SelectList(db.KHACHHANGs, "Id", "TenKH", donHang.MaDH);
            ViewBag.MaShip = new SelectList(db.Ships, "Id", "ShipName", donHang.MaShip);
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "Id", "TenKH", donHang.MaKH);
            return View(donHang);
        }

        // POST: Admin/DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaDH,MaKH,NgayDH,MaShip,DiaChiShip,TrangThai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaDH = new SelectList(db.KHACHHANGs, "Id", "TenKH", donHang.MaDH);
            ViewBag.MaShip = new SelectList(db.Ships, "Id", "ShipName", donHang.MaShip);
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "Id", "TenKH", donHang.MaKH);
            return View(donHang);
        }

        // GET: Admin/DonHangs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = await db.DonHangs.FindAsync(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: Admin/DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DonHang donHang = await db.DonHangs.FindAsync(id);
            db.DonHangs.Remove(donHang);
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
