using BiShop.Models;
using BiShop.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BiShop.Controllers
{
    public class ShopController : Controller
    {
        private DbBiLuxuryEntities db = new DbBiLuxuryEntities();
        // GET: Shop
        public ActionResult Index(int? id, int? page, int pageSize = 12, int sort = 1)
        {
            SearchProducts searchProducts = new SearchProducts();
            List<SanPhamViewModel> sanPhamViewModels = new List<SanPhamViewModel>();

            var sanpham = db.SanPhams.ToList();
            foreach (var item in sanpham)
            {
                SanPhamViewModel sanPhamViewModel = new SanPhamViewModel();
                sanPhamViewModel.sanPham = item;
                sanPhamViewModels.Add(sanPhamViewModel);
            }
            searchProducts.sanPhams = sanPhamViewModels;

            switch (sort)
            {
                case 1:
                    searchProducts.sanPhams = sanPhamViewModels.OrderBy(x => x.sanPham.Gia).ToList();
                    break;
                case 2:
                    searchProducts.sanPhams = sanPhamViewModels.OrderByDescending(x => x.sanPham.Gia).ToList();
                    break;
                default:
                    searchProducts.sanPhams = sanPhamViewModels.OrderBy(x => x.sanPham.Gia).ToList();
                    break;
            }

            if (id != null)
            {
                searchProducts.sanPhams = sanPhamViewModels.Where(x => x.sanPham.MaLSP == id).ToList();
            }

            ViewBag.lsp = Shops.getLoaiSanPham();

            ViewBag.ncc = Shops.getNCC();

            if (page == null) page = 1;
            int pageNumber = (page ?? 1);
            ViewBag.count = searchProducts.Count;
            return View(searchProducts.sanPhams.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ShopDetail(int? id)
        {
            var detail = db.SanPhams.FirstOrDefault(x => x.MaSP == id);
            SanPhamViewModel sanPhamViewModel = new SanPhamViewModel();
            sanPhamViewModel.sanPham = detail;

            var img = db.Imgs.FirstOrDefault(x => x.Link1 == detail.LinkAnh);
            if(img != null)
            {
                ViewBag.img1 = img.Link2;
                ViewBag.img2 = img.Link3;
                ViewBag.img3 = img.Link4;
            }
            else
            {
                ViewBag.img3 = "/Areas/assets/img/demo-size.jpg";
                ViewBag.img2 = "/assets/img/shop-details/thumb-2.png";
                ViewBag.img1 = "/assets/img/shop-details/thumb-3.png";
            }

            var review = db.Reviews.Where(x => x.MaSP == id).ToList();
            List<ReviewModel> reviewModels = new List<ReviewModel>();
            foreach(var item in review)
            {
                ReviewModel reviewModel = new ReviewModel();
                reviewModel.review = item;
                reviewModels.Add(reviewModel);
            }    
            ViewBag.review = reviewModels.OrderBy(x=>x.ThoiGian);

            var spCungLoai = db.SanPhams.Where(x => x.MaLSP == sanPhamViewModel.sanPham.MaLSP).Take(4).ToList();
            List<SanPhamViewModel> spcungloaiViewModel = new List<SanPhamViewModel>();
            foreach (var item in spCungLoai)
            {
                SanPhamViewModel spCungLoaiModel = new SanPhamViewModel();
                spCungLoaiModel.sanPham = item;
                spcungloaiViewModel.Add(spCungLoaiModel);
            }
            ViewBag.spCungLoai = spcungloaiViewModel;



            var category = db.LoaiSanPhams.FirstOrDefault(x => x.Id == detail.MaLSP);
            ViewBag.category = category.MieuTa;
            return View(sanPhamViewModel);
        }

        [HttpPost]
        public JsonResult Comment(string comment, string masp, string makh)
        {
            JsonResult json = new JsonResult();
            Review review = new Review();
            review.MaSP = Convert.ToInt32(masp);
            review.MaKH = Convert.ToInt32(makh);
            review.NoiDung = comment;
            review.NgayTao = DateTime.Now;
            db.Reviews.Add(review);
            db.SaveChanges();
            json.Data = new { Success = true }; 
            return json;
        }

        public JsonResult ListName(string term)
        {
            var data = db.SanPhams.Where(x => x.TenSP.Contains(term)).Select(x=>x.TenSP).ToList();
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string search)
        {
            List<SearchResult> infoAdd = new List<SearchResult>();

            ViewBag.search = search;

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var link = db.SanPhams.Where(s => s.TenSP == search);
            ViewBag.count = link.Count();
            watch.Stop();

            foreach (var item in link)
            {
                SearchResult info = new SearchResult();
                info.Id = item.MaSP;
                info.Link = "https://localhost:44319/Shop/ShopDetail/" + item.MaSP;
                info.Title = item.TenSP;
                info.Content = item.ChatLieu;
                info.Image = item.LinkAnh;
                infoAdd.Add(info);
            }

            ViewBag.time = $"{watch.ElapsedMilliseconds} ms";
            return View(infoAdd);
        }

        #region LocSanPham
        public ActionResult LocSanPham( string lspStr = "0", string gia = "0", int page = 0, int pageSize = 12)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            List<SanPham> sanPhams1 = new List<SanPham>();
            List<SanPham> sanPhams2 = new List<SanPham>();

            if (lspStr != "0")
            {
                int lsp = Convert.ToInt32(lspStr);
                sanPhams1.AddRange(db.SanPhams.Where(x => x.MaLSP == lsp).ToList());
            } 
            
            if(gia != "0")
            {
                string[] arrListStr = gia.Split(':');
                int giaLow = Convert.ToInt32(arrListStr[0]);
                int giaHigh = Convert.ToInt32(arrListStr[1]);
                sanPhams2.AddRange(sanPhams1.Where(x => x.Gia > giaLow && x.Gia < giaHigh).ToList());
            }

            if(lspStr != "0")
            {
                if (gia != "0")
                {
                    sanPhams = sanPhams2;
                }
                else if (gia == "0")
                {
                    sanPhams = sanPhams1;
                }
            } else if(lspStr == "0")
            {
                if (gia != "0")
                {
                    string[] arrListStr = gia.Split(':');
                    int giaLow = Convert.ToInt32(arrListStr[0]);
                    int giaHigh = Convert.ToInt32(arrListStr[1]);
                    sanPhams.AddRange(db.SanPhams.Where(x => x.Gia >= giaLow && x.Gia < giaHigh).ToList());
                }
                else if (gia == "0")
                {
                    sanPhams = db.SanPhams.ToList();
                }
            } 

            SearchProducts searchProducts = new SearchProducts();
            List<SanPhamViewModel> sanPhamViewModels = new List<SanPhamViewModel>();
            foreach (var item in sanPhams)
            {
                SanPhamViewModel sanPhamViewModel = new SanPhamViewModel();
                sanPhamViewModel.sanPham = item;
                sanPhamViewModels.Add(sanPhamViewModel);
            }
            searchProducts.sanPhams = sanPhamViewModels;

            ViewBag.lsp = Shops.getLoaiSanPham();
            ViewBag.ncc = Shops.getNCC();
            ViewBag.count = sanPhams.Count;
            if (page == 0) page = 1;
            int pageNumber = page == 0 ? 1 : page;
            return View("Index", searchProducts.sanPhams.ToPagedList(pageNumber, pageSize));
        }
        #endregion
    }
}