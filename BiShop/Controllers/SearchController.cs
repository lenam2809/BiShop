using BiShop.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiShop.Controllers
{
    public class SearchController : Controller
    {
        DbBiLuxuryEntities data = new DbBiLuxuryEntities();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SeachInClient(string search)
        {
            List<SearchResult> infoAdd = new List<SearchResult>();

            var searchString = RemoveUnicode(search).Trim();
            ViewBag.search = searchString;

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var link = data.SanPhams.Where(s => s.TenSP.Contains(searchString));
            ViewBag.count = link.Count();
            watch.Stop();

            var webGet = new HtmlWeb();
            foreach (var item in link)
            {
                SearchResult info = new SearchResult();
                info.Id = item.MaSP;
                info.Link = "https://localhost:44319/Shop/ShopDetail/" +  item.MaSP;
                info.Title = item.TenSP;
                info.Content = item.ChatLieu;
                info.Image = item.LinkAnh;
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