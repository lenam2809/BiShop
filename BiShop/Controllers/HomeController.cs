using BiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
        public async Task<ActionResult> Register(string name, string phone, string email, string pass, string role = "member")
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
                    await db.SaveChangesAsync();

                    KHACHHANG kh = new KHACHHANG();
                    kh.TenKH = name.Trim();
                    kh.Email = account.Email; ;
                    kh.Phone = phone.Trim();
                    kh.NgaySinh = DateTime.Today;
                    kh.GioiTinh = null;
                    kh.DiaChi = null;
                    kh.Vip = null;
                    kh.Photo = null;
                    db.KHACHHANGs.Add(kh);
                    await db.SaveChangesAsync();
                    ViewBag.success = "Đăng ký thành công";
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

        public ActionResult ProfileOfEmail(int id)
        {
            var profile = db.KHACHHANGs.FirstOrDefault(p => p.Id == id);
            ViewBag.TenKH = profile.TenKH;
            ViewBag.MaKH = profile.Id;
            ViewBag.NgaySinh = profile.NgaySinh;
            ViewBag.DiaChi = profile.DiaChi;
            ViewBag.GioiTinh = profile.GioiTinh;
            ViewBag.Phone = profile.Phone;
            ViewBag.Email = profile.Email;


            var JoinDH0 = from d in db.DonHangs
                          join c in db.CTDonHangs
                          on d.MaDH equals c.MaDH
                          select new { d.MaKH, c.MaSP, c.SoLuong, d.NgayDH };
            var JoinDH = JoinDH0.Where(s => s.MaKH == profile.Id);

            var JoinSP = from a in JoinDH
                         join s in db.SanPhams
                         on a.MaSP equals s.MaSP
                         select new LSThanhToan { TenSP = s.TenSP, SoLuong = (int)a.SoLuong, Gia = (int)s.Gia, NgayDH = (DateTime)a.NgayDH };
            ViewBag.donhang = JoinSP;
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
                return RedirectToAction("ProfileOfEmail", new { email = collection.Email });
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
    }
}