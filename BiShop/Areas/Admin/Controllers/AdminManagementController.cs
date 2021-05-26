using BiShop.Models;
using BiShop.ViewModel;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BiShop.Areas.Admin.Controllers
{
    public class AdminManagementController : Controller
    {
        private DbBiLuxuryEntities data = new DbBiLuxuryEntities();
        // GET: Admin/AdminManagement
        [Authorize(Roles = "admin, emp")]
        public ActionResult Index()
        {
            ViewBag.TongDoanhThuTrongNgay =  DoanhThuTrongNgay(DateTime.Today);
            ViewBag.KhachHangMoiTrongNgay = KhachHangMoiTrongNgay(DateTime.Today);
            ViewBag.SanPhamXuatTrongNgay = SanPhamXuatTrongNgay(DateTime.Today);

            ViewBag.TongDoanhThuTrongNam = DoanhThuTrongNam(DateTime.Today);
            ViewBag.KhachHangMoiTrongNam = KhachHangMoiTrongNam(DateTime.Today);
            ViewBag.SanPhamXuatTrongNam = SanPhamXuatTrongNam(DateTime.Today);

            ViewBag.KhachHangMoiQuaCacNam = JsonConvert.SerializeObject(KhachHangMoiQuaCacNam(DateTime.Today));
            ViewBag.DoanhThuQuaCacNam = JsonConvert.SerializeObject(DoanhThuQuaCacNam(DateTime.Today));
            ViewBag.SanPhamXuatQuaCacNam = JsonConvert.SerializeObject(SanPhamXuatQuaCacNam(DateTime.Today));

            ViewBag.DoanhThu7Ngay = JsonConvert.SerializeObject(DoanhThu7Ngay(DateTime.Today));
            ViewBag.ChiPhi7Ngay = JsonConvert.SerializeObject(chiPhi7Ngay(DateTime.Today));
            ViewBag.Trong7Ngay = JsonConvert.SerializeObject(Trong7Ngay(DateTime.Today));

            ViewBag.DoanhThu12Thang = JsonConvert.SerializeObject(DoanhThu12Thang(DateTime.Today));
            ViewBag.ChiPhi12Thang = JsonConvert.SerializeObject(ChiPhi12Thang(DateTime.Today));

            BaocaoTongQuan bc = new BaocaoTongQuan();
            var sp = bc.sanPhamNoiBat;
            ViewBag.tensp = sp.TenSP;
            ViewBag.spnbImg = sp.LinkAnh;
            ViewBag.spnbISL = bc.SLsanPhamNoiBat;

            return View();       
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAdmin(string email, string password, string returnUrl)
        {
            var dataItems = data.Accounts.FirstOrDefault(x => x.Email == email
            && x.Password == password);
            if (dataItems != null)
            {
                Session["Role"] = Convert.ToString(dataItems.Role);
                FormsAuthentication.SetAuthCookie(dataItems.Email, false);
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
                    return RedirectToAction("Index", "AdminManagement", new { area = "Admin" });
                }
            }
            else
            {
                ModelState.AddModelError("AdminLogin", "Tài khoản hoặc mật khẩu không chính xác");
                return View("~/Areas/Admin/Views/AdminManagement/LoginAdmin.cshtml");
            }
        }

        [Authorize]
        public ActionResult SignOutAdmin()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginAdmin", "AdminManagement");
        }

        [HttpPost, ValidateInput(false)]
        public async Task<string> Upload(HttpPostedFileBase file)
        {
            //xu ly upload
            file.SaveAs(Server.MapPath("~/Upload/" + file.FileName));
            await Task.Delay(500);
            string result = "/Upload/" + file.FileName;
            return result;
        }

        #region Dashboard
        public string DoanhThuTrongNgay(DateTime dateTime)
        {
            //var item = data.CTDonHangs.Where(x => DateTime.Compare((DateTime)x.DonHang.NgayDH, dateTime) >= 0 && x.DonHang.TrangThai == "Đơn hoàn thành").ToList();
            var item = data.CTDonHangs.Where(x => DateTime.Compare((DateTime)x.DonHang.NgayDH, dateTime) >= 0).ToList();

            var sums = 0;
            foreach(var i in item)
            {
                sums += Convert.ToInt32(i.SanPham.Gia * i.SoLuong);
            }    
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(info, "{0:c3}", sums);
        }

        public string DoanhThuTrongNam(DateTime dateTime)
        {
            var item = data.CTDonHangs.Where(x => x.DonHang.NgayDH.Value.Year == dateTime.Year && x.DonHang.TrangThai == "Đơn hoàn thành").ToList();
            var sums = 0;
            foreach (var i in item)
            {
                sums += Convert.ToInt32(i.SanPham.Gia * i.SoLuong);
            }
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(info, "{0:c3}", sums);
        }

        public List<int> DoanhThuQuaCacNam(DateTime dateTime)
        {
            List<int> sums = new List<int>();   
            for(int year = dateTime.Year-7; year < dateTime.Year; year++ )
            {
                var item = data.CTDonHangs.Where(x => x.DonHang.NgayDH.Value.Year == year && x.DonHang.TrangThai == "Đơn hoàn thành").ToList();
                var sum = 0;
                foreach (var z in item)
                {
                    sum += Convert.ToInt32(z.SanPham.Gia * z.SoLuong);
                }
                var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                sums.Add(sum);
            }
            return sums;
        }

        public List<int> KhachHangMoiQuaCacNam(DateTime dateTime)
        {
            List<int> sums = new List<int>();
            for (int year = dateTime.Year - 7; year < dateTime.Year; year++)
            {
                var item = data.KHACHHANGs.Where(x => x.NgayTao.Value.Year == year).ToList();
                sums.Add(item.Count);
            }
            return sums;
        }

        public int KhachHangMoiTrongNgay(DateTime dateTime)
        {
            var item = data.KHACHHANGs.Where(x => DateTime.Compare(x.NgayTao.Value, dateTime) >= 0);
            return item.Count();
        }

        public int KhachHangMoiTrongNam(DateTime dateTime)
        {
            var item = data.KHACHHANGs.Where(x => x.NgayTao.Value.Year >= dateTime.Year);
            return item.Count();
        }

        public int SanPhamXuatTrongNgay(DateTime dateTime)
        {
            //var item = data.CTDonHangs.Where(x => DateTime.Compare((DateTime)x.DonHang.NgayDH, dateTime) >= 0 && x.DonHang.TrangThai == "Đơn hoàn thành").ToList();
            var item = data.CTDonHangs.Where(x => DateTime.Compare((DateTime)x.DonHang.NgayDH, dateTime) >= 0).ToList();

            var sums = 0;
            foreach (var i in item)
            {
                sums += Convert.ToInt32(i.SoLuong);
            }
            return sums;
        }

        public int SanPhamXuatTrongNam(DateTime dateTime)
        {
            var item = data.CTDonHangs.Where(x => x.DonHang.NgayDH.Value.Year >= dateTime.Year && x.DonHang.TrangThai == "Đơn hoàn thành").ToList();
            var sums = 0;
            foreach (var i in item)
            {
                sums += Convert.ToInt32(i.SoLuong);
            }
            return sums;
        }

        public List<int> SanPhamXuatQuaCacNam(DateTime dateTime)
        {
            List<int> sums = new List<int>();
            for (int year = dateTime.Year - 7; year < dateTime.Year; year++)
            {
                var item = data.CTDonHangs.Where(x => x.DonHang.NgayDH.Value.Year == year && x.DonHang.TrangThai == "Đơn hoàn thành").ToList();
                var sum = 0;
                foreach (var z in item)
                {
                    sum += Convert.ToInt32(z.SoLuong);
                }
                sums.Add(sum);
            }
            return sums;
        }

        #endregion

        #region 7 ngay

        public List<int> DoanhThu7Ngay(DateTime dateTime)
        {
            List<int> sums = new List<int>();
            for (DateTime day = dateTime.AddDays(-7); day <= dateTime.Date; day = day.AddDays(1))
            {
                var item = data.CTDonHangs.Where(x => DbFunctions.TruncateTime(x.DonHang.NgayDH) == day.Date).ToList();
                var sum = 0;
                foreach (var z in item)
                {
                    sum += Convert.ToInt32(z.SanPham.Gia * z.SoLuong);
                }
                sums.Add(sum);
            }
            return sums;
        }

        public List<int> chiPhi7Ngay(DateTime dateTime)
        {
            List<int> sums = new List<int>();
            for (DateTime day = dateTime.AddDays(-7); day <= dateTime.Date; day = day.AddDays(1))
            {
                var item = data.CTNhapSanPhams.Where(x => DbFunctions.TruncateTime(x.NhapSanPham.NgayNhap) == day.Date).ToList();
                var sum = 0;
                foreach (var z in item)
                {
                    sum += Convert.ToInt32(z.GiaNhap * z.SoLuong);
                }
                sums.Add(sum);
            }
            return sums;
        }

        public List<string> Trong7Ngay(DateTime dateTime)
        {
            List<string> dates = new List<string>();
            for (DateTime day = dateTime.AddDays(-7); day <= dateTime.Date; day = day.AddDays(1))
            {
                var item = data.CTDonHangs.Where(x => DbFunctions.TruncateTime(x.DonHang.NgayDH) == day.Date).ToList();
                var s = day.DayOfWeek;
                dates.Add(s.ToString().Substring(0,3));
            }
            return dates;
        }

        #endregion

        #region 12 thang

        public List<int> DoanhThu12Thang(DateTime dateTime)
        {
            List<int> sums = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                var item = data.CTDonHangs.Where(x => x.DonHang.NgayDH.Value.Month == month && x.DonHang.NgayDH.Value.Year == dateTime.Year).ToList();
                var sum = 0;
                foreach (var z in item)
                {
                    sum += Convert.ToInt32(z.SanPham.Gia * z.SoLuong);
                }
                sums.Add(sum);
            }
            return sums;
        }

        public List<int> ChiPhi12Thang(DateTime dateTime)
        {
            List<int> sums = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                var item = data.CTNhapSanPhams.Where(x => x.NhapSanPham.NgayNhap.Value.Month == month && x.NhapSanPham.NgayNhap.Value.Year == dateTime.Year).ToList();
                var sum = 0;
                foreach (var z in item)
                {
                    sum += Convert.ToInt32(z.GiaNhap * z.SoLuong);
                }
                sums.Add(sum);
            }
            return sums;
        }

        #endregion

        #region Doanh thu và chi phí trong tuần

        public string DoanhThuTrongTuan(DateTime dateTime)
        {
            var item = data.CTDonHangs.Where(x => DateTime.Compare((DateTime)x.DonHang.NgayDH, dateTime) >= 0 && x.DonHang.TrangThai == "Đơn hoàn thành").ToList();
            var sums = 0;
            foreach (var i in item)
            {
                sums += Convert.ToInt32(i.SanPham.Gia * i.SoLuong);
            }
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(info, "{0:c3}", sums);
        }

        public string ChiPhiTrongTuan(DateTime dateTime)
        {
            var item = data.CTDonHangs.Where(x => DateTime.Compare((DateTime)x.DonHang.NgayDH, dateTime) >= 0 && x.DonHang.TrangThai == "Đơn hoàn thành").ToList();
            var sums = 0;
            foreach (var i in item)
            {
                sums += Convert.ToInt32(i.SanPham.Gia * i.SoLuong);
            }
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(info, "{0:c3}", sums);
        }

        #endregion

        [HttpGet]
        public ActionResult SeachInfo(string search)
        {
            List<SearchResult> infoAdd = new List<SearchResult>();

            var searchString = RemoveUnicode(search).Trim();
            var position = searchString.Replace(" ", "-");
            ViewBag.search = searchString;

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var link = data.Searches.Where(s => s.Info.Contains(position));
            ViewBag.count = link.Count();
            watch.Stop();

            var webGet = new HtmlWeb();
            foreach (var item in link)
            {
                SearchResult info = new SearchResult();
                info.Id = item.Id;
                info.Link = item.Link;
                var document = webGet.Load(item.Link);
                info.Title = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                info.Content = "Những dấu ấn nổi bật nhất của Biluxury tại sân chơi thời trang VIFW đã có màn ra mắt vô cùng ấn tượng với giới truyền thông và mộ điệu.";
                infoAdd.Add(info);
            }

            ViewBag.time = $"{watch.ElapsedMilliseconds} ms";
            return View(infoAdd);
        }

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}