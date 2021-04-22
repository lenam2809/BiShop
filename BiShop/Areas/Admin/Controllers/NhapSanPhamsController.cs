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
    public class NhapSanPhamsController : Controller
    {
        private DbBiLuxuryEntities db = new DbBiLuxuryEntities();

        // GET: Admin/NhapSanPhams
        public async Task<ActionResult> Index()
        {
            return View(await db.NhapSanPhams.ToListAsync());
        }

        // GET: Admin/NhapSanPhams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhapSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MaSP,NgayNhap,SoLuong,GiaNhap")] NhapSanPham nhapSanPham)
        {
            if (ModelState.IsValid)
            {
                db.NhapSanPhams.Add(nhapSanPham);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nhapSanPham);
        }

        // GET: Admin/NhapSanPhams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapSanPham nhapSanPham = await db.NhapSanPhams.FindAsync(id);
            if (nhapSanPham == null)
            {
                return HttpNotFound();
            }
            return View(nhapSanPham);
        }

        // POST: Admin/NhapSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MaSP,NgayNhap,SoLuong,GiaNhap")] NhapSanPham nhapSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhapSanPham).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nhapSanPham);
        }

        // GET: Admin/NhapSanPhams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapSanPham nhapSanPham = await db.NhapSanPhams.FindAsync(id);
            if (nhapSanPham == null)
            {
                return HttpNotFound();
            }
            return View(nhapSanPham);
        }

        // POST: Admin/NhapSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NhapSanPham nhapSanPham = await db.NhapSanPhams.FindAsync(id);
            db.NhapSanPhams.Remove(nhapSanPham);
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
