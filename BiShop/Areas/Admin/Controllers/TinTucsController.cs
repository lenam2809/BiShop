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
    public class TinTucsController : Controller
    {
        private DbBiLuxuryEntities db = new DbBiLuxuryEntities();

        // GET: Admin/TinTucs
        public async Task<ActionResult> Index()
        {
            var tinTucs = db.TinTucs.Include(t => t.LoaiSanPham).Include(t => t.LoaiTin);
            return View(await tinTucs.ToListAsync());
        }

        // GET: Admin/TinTucs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = await db.TinTucs.FindAsync(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Create
        public ActionResult Create()
        {
            ViewBag.LoaiSP = new SelectList(db.LoaiSanPhams, "Id", "TenLoai");
            ViewBag.MaLoaiTin = new SelectList(db.LoaiTins, "Id", "TenLoaiTin");
            return View();
        }

        // POST: Admin/TinTucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,TieuDe,LinkAnh,NoiDung,NgayTao,NguoiTao,LuotXem,LoaiSP,MaLoaiTin")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.TinTucs.Add(tinTuc);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiSP = new SelectList(db.LoaiSanPhams, "Id", "TenLoai", tinTuc.LoaiSP);
            ViewBag.MaLoaiTin = new SelectList(db.LoaiTins, "Id", "TenLoaiTin", tinTuc.MaLoaiTin);
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = await db.TinTucs.FindAsync(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiSP = new SelectList(db.LoaiSanPhams, "Id", "TenLoai", tinTuc.LoaiSP);
            ViewBag.MaLoaiTin = new SelectList(db.LoaiTins, "Id", "TenLoaiTin", tinTuc.MaLoaiTin);
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TieuDe,LinkAnh,NoiDung,NgayTao,NguoiTao,LuotXem,LoaiSP,MaLoaiTin")] TinTuc tinTuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tinTuc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiSP = new SelectList(db.LoaiSanPhams, "Id", "TenLoai", tinTuc.LoaiSP);
            ViewBag.MaLoaiTin = new SelectList(db.LoaiTins, "Id", "TenLoaiTin", tinTuc.MaLoaiTin);
            return View(tinTuc);
        }

        // GET: Admin/TinTucs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TinTuc tinTuc = await db.TinTucs.FindAsync(id);
            if (tinTuc == null)
            {
                return HttpNotFound();
            }
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TinTuc tinTuc = await db.TinTucs.FindAsync(id);
            db.TinTucs.Remove(tinTuc);
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
