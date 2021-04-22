using BiShop.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            ViewBag.TongDoanhThuTrongNgay =  DoanhThuTrongNgay(DateTime.Today);
            ViewBag.KhachHangMoiTrongNgay = KhachHangMoiTrongNgay(DateTime.Today);
            ViewBag.SanPhamXuatTrongNgay =  SanPhamXuatTrongNgay(DateTime.Today);
            return View();       
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl)
        {

            var dataItems = data.Accounts.FirstOrDefault(x => x.Email == email
            && x.Password == password);
            if (dataItems != null)
            {
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
                    return RedirectToAction("index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View();
            }
        }

        [Authorize]
        public ActionResult SignOutAdmin()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminManagement");
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

        public string DoanhThuTrongNgay(DateTime dateTime)
        {
            string result = null;
            var item = data.DonHangs.Where(x => x.NgayDH == dateTime);

            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            result = String.Format(info, "{0:c3}", 300);
            return result;
        }

        public int KhachHangMoiTrongNgay(DateTime dateTime)
        {
            int result = 0;
            var item = data.KHACHHANGs.Where(x => x.Vip == dateTime.ToString());
            result = 150;
            return result;
        }

        public int SanPhamXuatTrongNgay(DateTime dateTime)
        {
            int result = 0;
            var item = data.KHACHHANGs.Where(x => x.Vip == dateTime.ToString());
            result = 150;
            return result;
        }

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