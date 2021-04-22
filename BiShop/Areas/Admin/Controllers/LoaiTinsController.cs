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
    public class LoaiTinsController : Controller
    {
        private DbBiLuxuryEntities db = new DbBiLuxuryEntities();

        // GET: Admin/LoaiTins
        public async Task<ActionResult> Index()
        {
            return View(await db.LoaiTins.ToListAsync());
        }

        // GET: Admin/LoaiTins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiTins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TenLoaiTin")] LoaiTin loaiTin)
        {
            if (ModelState.IsValid)
            {
                db.LoaiTins.Add(loaiTin);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loaiTin);
        }

        // GET: Admin/LoaiTins/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTin loaiTin = await db.LoaiTins.FindAsync(id);
            if (loaiTin == null)
            {
                return HttpNotFound();
            }
            return View(loaiTin);
        }

        // POST: Admin/LoaiTins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LoaiTin loaiTin = await db.LoaiTins.FindAsync(id);
            db.LoaiTins.Remove(loaiTin);
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
