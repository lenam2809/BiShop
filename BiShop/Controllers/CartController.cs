using BiShop.Models;
using BiShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BiShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        private DbBiLuxuryEntities data = new DbBiLuxuryEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<Cart>();
            var total = 0;

            if (cart != null)
            {
                list = (List<Cart>)cart;
            }
            foreach (var item in list)
            {
                total += item.TongTien;
            }
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            ViewBag.total = String.Format(info, "{0:c3}", total);


            return View(list);
        }

        [HttpPost]
        public JsonResult UpDate(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<Cart>>(cartModel);
            var sessionCart = (List<Cart>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsItem = jsonCart.SingleOrDefault(x => x.SanPhamViewModel.sanPham.MaSP == item.SanPhamViewModel.sanPham.MaSP);
                
                if (jsItem != null)
                {
                    item.SoLuong = jsItem.SoLuong;
                }
            }
            Session[CartSession] = sessionCart;

            return Json(new
            {
                Status = true
            });
        }

        [HttpPost]
        public JsonResult Delete(long masp)
        {
            var sessionCart = (List<Cart>)Session[CartSession];
            sessionCart.RemoveAll(x => x.SanPhamViewModel.sanPham.MaSP == masp);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                Status = true
            });
        }


        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                Status = true
            });
        }

        public ActionResult AddItem(int masp, int soluong)
        {
            var sanphams = data.SanPhams.FirstOrDefault(s => s.MaSP == masp);

            SanPhamViewModel sanPhamViewModel = new SanPhamViewModel();
            sanPhamViewModel.sanPham = sanphams;

            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<Cart>)cart;
                if (list.Exists(x => x.SanPhamViewModel.sanPham.MaSP == masp))
                {
                    foreach (var item in list)
                    {
                        if (item.SanPhamViewModel.sanPham.MaSP == masp)
                        {
                            item.SoLuong += soluong;
                        }
                    }
                }
                else
                {
                    var item = new Cart();
                    item.SanPhamViewModel = sanPhamViewModel;
                    item.SoLuong = soluong;
                    list.Add(item);
                    //gan vao session
                    Session[CartSession] = list;
                }
            }
            else
            {
                //tao moi cart
                var item = new Cart();
                item.SanPhamViewModel = sanPhamViewModel;
                item.SoLuong = soluong;
                var list = new List<Cart>();
                list.Add(item);
                //gan vao session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public ActionResult ThanhToan()
        {
            var cart = Session[CartSession];
            var list = new List<Cart>();
            var total = 0;

            if (cart != null)
            {
                list = (List<Cart>)cart;
            }
            if (User.Identity.IsAuthenticated)
            {
                string emailkh = HttpContext.User.Identity.Name;
                var kh = data.KHACHHANGs.FirstOrDefault(k => k.Email == emailkh);
                ViewBag.tenkh = kh.TenKH;
                ViewBag.diachi = kh.DiaChi;
                ViewBag.email = emailkh;
                ViewBag.phone = kh.Phone;
            }

            var ship = data.Ships.ToList();
            ViewBag.ship = ship;

            foreach (var item in list)
            {
                total += item.TongTien;
            }
            var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            ViewBag.Tongtien = String.Format(info, "{0:c3}", total);

            return View(list);
        }

        [HttpPost]
        public ActionResult ThanhToan(string name, string diachi, string email, string phone)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var kh = data.KHACHHANGs.FirstOrDefault(k => k.Email == email);
                    var order = new DonHang()
                    {
                        MaKH = kh.Id,
                        NgayDH = DateTime.Now,
                        MaShip = 1,
                        DiaChiShip = diachi,
                        TrangThai = "Đơn mới",
                        GhiChu = name + " - " + phone + " - " + DateTime.Now.ToString(),
                    };
                    data.DonHangs.Add(order);
                    data.SaveChanges();

                    var cart = (List<Cart>)Session[CartSession];
                    List<CTDonHang> orderDetails = new List<CTDonHang>();
                    foreach (var item in cart)
                    {
                        var ctdh = new CTDonHang()
                        {
                            MaDH = order.MaDH,
                            MaSP = item.SanPhamViewModel.sanPham.MaSP,
                            SoLuong = item.SoLuong,
                            GhiChu = item.SanPhamViewModel.sanPham.TenSP + " - " + item.SoLuong.ToString(),
                        };
                        orderDetails.Add(ctdh);
                        SanPham sanpham = data.SanPhams.FirstOrDefault(x => x.MaSP == item.SanPhamViewModel.sanPham.MaSP);
                        sanpham.SoLuong = sanpham.SoLuong - item.SoLuong;
                        data.Entry(sanpham).State = EntityState.Modified;
                        data.SaveChanges();
                    }
                    data.CTDonHangs.AddRange(orderDetails);
                    data.SaveChanges();
                    TempData["ThanhToan"] = "Thanh toán thành công";
                    cart.Clear();
                    return RedirectToAction("Index");
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

        public ActionResult CartPartial()
        {
            Cart model = new Cart();

            int qty = 0;
            if (Session[CartSession] != null)
            {
                var list = (List<Cart>)Session[CartSession];
                foreach (var item in list)
                {
                    qty += item.SoLuong;
                }
                model.SoLuong = qty;
            }
            else
            {
                model.SoLuong = 0;
            }

            return PartialView(model);
        }


    }
}