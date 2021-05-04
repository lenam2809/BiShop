using BiShop.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BiShop.Controllers
{
    public class HomeController : Controller
    {
        DbBiLuxuryEntities db = new DbBiLuxuryEntities();
        public ActionResult Index()
        {
            //Loai san pham
            var category = db.LoaiSanPhams.ToList();
            ViewBag.category = category;

            //Top 4 san pham moi
            var top4product = db.SanPhams.Take(4).ToList();
            ViewBag.top4product = top4product;

            //Top 4 san pham ban chạy nhất
            var top4banchay = db.SanPhams.OrderBy(x=>x.Gia).Take(4).ToList();
            ViewBag.top4banchay = top4banchay;

            //Top 4 san pham ban chạy nhất
            var top4sales = db.SanPhams.OrderByDescending(x => x.Gia).Take(4).ToList();
            ViewBag.top4sales = top4sales;

            //top 3 blogs
            var top3tintuc = db.TinTucs.Take(3).ToList();
            ViewBag.top3tintuc = top3tintuc;

            return View();
        }

        public ActionResult About()
        {
            var CountKH = db.KHACHHANGs.Count();
            ViewBag.CountKH = CountKH;

            var CountSP = db.SanPhams.Count();
            ViewBag.CountSP = CountSP;
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl)
        {
            var check = db.Accounts.FirstOrDefault(x => x.Email == email
            && x.Password == password);
            if (check != null)
            {
                var khachhang = db.KHACHHANGs.FirstOrDefault(x => x.Email == email);
                Session["Name"] = khachhang.TenKH.ToString();
                Session["Id"] = khachhang.Id;
                FormsAuthentication.SetAuthCookie(check.Email, false);
                if (Url.IsLocalUrl(returnUrl)
                    && returnUrl.Length > 1
                    && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//")
                    && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("index");
                }
            }
            else
            {
                ModelState.AddModelError("test", "Tài khoản hoặc mật khẩu không chính xác !");
                return View();
            }
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string name, string phone, string email, string pass, string role = "member")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var check = db.Accounts.FirstOrDefault(s => s.Email == email.Trim());
                    if (check == null)
                    {
                        Account account = new Account();
                        account.Email = email.Trim();
                        account.Password = pass.Trim();
                        account.Role = role;
                        db.Accounts.Add(account);
                        db.SaveChanges();

                        KHACHHANG kh = new KHACHHANG();
                        kh.TenKH = name.Trim();
                        kh.Email = account.Email;
                        kh.Phone = phone.Trim();
                        kh.NgaySinh = DateTime.Today;
                        kh.GioiTinh = "Nam";
                        kh.DiaChi = "Hà Nội";
                        kh.Vip = "Không";
                        kh.Photo = "/Upload/img_avatar.png";
                        kh.NgayTao = DateTime.Now;
                        db.KHACHHANGs.Add(kh);
                        db.SaveChanges();
                        ViewBag.success = "Đăng ký thành công";
                        TempData["Success"] = "Đăng ký thành công";
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        ViewBag.error = "Email đã tồn tại";
                        return View();
                    }
                }
                return View();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        public ActionResult ProfileOfEmail(int id)
        {
            var profile = db.KHACHHANGs.FirstOrDefault(p => p.Id == id);
            ViewBag.avt = profile.Photo;
            ViewBag.TenKH = profile.TenKH;
            ViewBag.MaKH = profile.Id;
            ViewBag.NgaySinh = profile.NgaySinh;
            ViewBag.DiaChi = profile.DiaChi;
            ViewBag.GioiTinh = profile.GioiTinh;
            ViewBag.Phone = profile.Phone;
            ViewBag.Email = profile.Email;

            return View();
        }


        public ActionResult EditProfile(int id)
        {
            var proffile = db.KHACHHANGs.FirstOrDefault(p => p.Id == id);
            return View(proffile);
        }

        [HttpPost]
        public ActionResult EditProfile(int id, KHACHHANG collection)
        {
            try
            {
                // TODO: Add update logic here
                KHACHHANG Sp = db.KHACHHANGs.Single(s => s.Id == id);
                Sp.TenKH = collection.TenKH;
                Sp.NgaySinh = collection.NgaySinh;
                Sp.DiaChi = collection.DiaChi;
                Sp.Email = collection.Email;
                Sp.GioiTinh = collection.GioiTinh;
                Sp.Phone = collection.Phone;
                db.SaveChanges();
                return RedirectToAction("ProfileOfEmail", new { id = id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult LichSuThanhToan(int id)
        {
            var donhang = db.DonHangs.Where(p => p.MaKH == id);
            return View(donhang);
        }

        public ActionResult ChiTietDH(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ctdonHang = db.CTDonHangs.Where(x => x.MaDH == id).ToList();
            if (ctdonHang == null)
            {
                return HttpNotFound();
            }
            return View(ctdonHang);
        }

        public ActionResult XemHoaDon(int id)
        {
            var donhang = db.DonHangs.FirstOrDefault(x => x.MaDH == id);
            var ctdh = db.CTDonHangs.Where(x => x.MaDH == id);
            var total = 0;
            foreach (var item in ctdh)
            {
                total += (int)((item.SanPham.Gia - (item.SanPham.LoaiSanPham.KhuyenMai.Discount * item.SanPham.Gia / 100))*item.SoLuong);
            }
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            ViewBag.Tongtien = String.Format(info, "{0:c3}", total);
            ViewBag.ctdh = ctdh;
            return View(donhang);
        }


        public ActionResult XuatHoaDon(int id)
        {
            var donhang = db.DonHangs.FirstOrDefault(x => x.MaDH == id);
            var ctdh = db.CTDonHangs.Where(x => x.MaDH == id);
            var total = 0;
            foreach (var item in ctdh)
            {
                total += (int)((item.SanPham.Gia - (item.SanPham.LoaiSanPham.KhuyenMai.Discount * item.SanPham.Gia / 100)) * item.SoLuong);
            }
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            ViewBag.Tongtien = String.Format(info, "{0:c3}", total);
            ViewBag.ctdh = ctdh;
            var report = new PartialViewAsPdf("~/Views/ListPartial/_HoaDon.cshtml", donhang);
            return report;
        }

        public ActionResult Cancel(int id)
        {
            var donhang = db.DonHangs.FirstOrDefault(x => x.MaDH == id);
            donhang.TrangThai = "Đơn hủy";
            db.SaveChanges();
            return RedirectToAction("LichSuThanhToan", new { id = donhang.MaKH });
        }
    }
}