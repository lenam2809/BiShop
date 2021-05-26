using BiShop.Models;
using BiShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiShop.Areas.Admin.Controllers
{
    public class BaoCaoThongKeController : Controller
    {
        private DbBiLuxuryEntities data = new DbBiLuxuryEntities();
        // GET: Admin/BaoCaoThongKe
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult BaoCaoNhap()
        {
            var ctnh = data.CTNhapSanPhams;

            List<BaoCaoNhap> baocaonhap = new List<BaoCaoNhap>();
            foreach (var item in ctnh)
            {
                BaoCaoNhap bcn = new BaoCaoNhap();
                bcn.getCTNhapSanPham = item;
                baocaonhap.Add(bcn);
            }

            var bcns = baocaonhap.GroupBy(x => new { x.TenSP, x.NgayNhap, x.GiaNhap })
                        .Select(y => new BCN
                        {
                            TenSP = y.Key.TenSP,
                            SoLuongNhap = y.Sum(c => c.SoLuongNhap),
                            GiaNhap = y.Key.GiaNhap,
                            NgayNhap = y.Key.NgayNhap,
                            ChiPhiNhap = y.Sum(c => c.ChiPhiNhap),
                        }).ToList();

            var sumSoLuongNhap = 0;
            var sumChiPhiNhap = 0;
            foreach (var list in bcns)
            {
                sumSoLuongNhap += list.SoLuongNhap;
                sumChiPhiNhap += list.ChiPhiNhap;
            }

            ViewBag.sumSoLuongNhap = sumSoLuongNhap;
            ViewBag.sumChiPhiNhap = sumChiPhiNhap;
            return View(bcns.ToList());
        }

        public ActionResult BaoCaoXuat(DateTime? TuNgay, DateTime? DenNgay)
        {
            var ctdh = data.CTDonHangs.ToList();
            if (TuNgay != null)
            {
                ctdh = ctdh.Where(x => x.DonHang.NgayDH > TuNgay).ToList();
            }
            if (DenNgay != null)
            {
                ctdh = ctdh.Where(x => x.DonHang.NgayDH < DenNgay).ToList();
            }

            List<BaoCao> baocaos = new List<BaoCao>();
            foreach(var item in ctdh)
            {
                BaoCao bc = new BaoCao();
                bc.ctDonHang = item;
                baocaos.Add(bc);
            }

            var bcs = baocaos.GroupBy(x => x.TenSP)
                        .Select(y => new BC
                        {
                            TenSP = y.Key,
                            SoLuongThuc = y.Sum(c => c.SoLuongThuc),
                            DoanhThu = y.Sum(c => c.DoanhThu),
                            GiamGia = y.Sum(c => c.GiamGia),
                            DoanhThuThuc = y.Sum(c => c.DoanhThuThuc),
                            TongDoanhThu = y.Sum(c => c.TongDoanhThu),
                        }).ToList();

            var sumSoLuongThuc = 0;
            var sumDoanhThu = 0;
            var sumGiamGia = 0;
            foreach (var list in bcs)
            {
                sumDoanhThu += list.DoanhThu;
                sumSoLuongThuc += list.SoLuongThuc;
                sumGiamGia += list.GiamGia;
            }

            ViewBag.sumSoLuongThuc = sumSoLuongThuc;
            ViewBag.sumDoanhThu = sumDoanhThu;
            ViewBag.sumGiamGia = sumGiamGia;
            ViewBag.sumDoanhThuThuc = sumDoanhThu - sumGiamGia;
            return View(bcs.ToList());
        }

        public ActionResult BaoCaoTon()
        {
            var sp = data.SanPhams.ToList();
            return View(sp.ToList());
        }

        public ActionResult BaoCaoTheoDanhMuc(DateTime? TuNgay, DateTime? DenNgay)
        {
            var ctdh = data.CTDonHangs.ToList();
            if (TuNgay != null)
            {
                ctdh = ctdh.Where(x=>x.DonHang.NgayDH > TuNgay).ToList();
            }
            if(DenNgay != null)
            {
                ctdh = ctdh.Where(x => x.DonHang.NgayDH < DenNgay).ToList();
            }    


            List<BaoCao> baocaos = new List<BaoCao>();
            foreach (var item in ctdh)
            {
                BaoCao bc = new BaoCao();
                bc.ctDonHang = item;
                baocaos.Add(bc);
            }

            var bcs = baocaos.GroupBy(x => x.LSP)
                        .Select(y => new BCTheoDanhMuc
                        {
                            LSP = y.Key,
                            SoLuongThuc = y.Sum(c => c.SoLuongThuc),
                            DoanhThu = y.Sum(c => c.DoanhThu),
                            GiamGia = y.Sum(c => c.GiamGia),
                            DoanhThuThuc = y.Sum(c => c.DoanhThuThuc),
                            TongDoanhThu = y.Sum(c => c.TongDoanhThu),
                        }).ToList();

            var sumSoLuongThuc = 0;
            var sumDoanhThu = 0;
            var sumGiamGia = 0;
            foreach (var list in bcs)
            {
                sumDoanhThu += list.DoanhThu;
                sumSoLuongThuc += list.SoLuongThuc;
                sumGiamGia += list.GiamGia;
            }

            ViewBag.sumSoLuongThuc = sumSoLuongThuc;
            ViewBag.sumDoanhThu = sumDoanhThu;
            ViewBag.sumGiamGia = sumGiamGia;
            ViewBag.sumDoanhThuThuc = sumDoanhThu - sumGiamGia;

            ViewBag.TuNgay = TuNgay;
            ViewBag.DenNgay = DenNgay;
            return View(bcs.ToList());
        }

        public ActionResult BaoCaoNhapTheoDanhMuc(DateTime? TuNgay, DateTime? DenNgay)
        {
            var ctnh = data.CTNhapSanPhams.ToList();
            if (TuNgay != null)
            {
                ctnh = ctnh.Where(x => x.NhapSanPham.NgayNhap > TuNgay).ToList();
            }
            if (DenNgay != null)
            {
                ctnh = ctnh.Where(x => x.NhapSanPham.NgayNhap < DenNgay).ToList();
            }

            List<BaoCaoNhap> baocaonhap = new List<BaoCaoNhap>();
            foreach (var item in ctnh)
            {
                BaoCaoNhap bcn = new BaoCaoNhap();
                bcn.getCTNhapSanPham = item;
                baocaonhap.Add(bcn);
            }

            var bcns = baocaonhap.GroupBy(x => new { x.LSP })
                        .Select(y => new BCNCategory
                        {
                            LSP = y.Key.LSP,
                            SoLuongNhap = y.Sum(c => c.SoLuongNhap),
                            ChiPhiNhap = y.Sum(c => c.ChiPhiNhap)
                        }).ToList();

            var sumSoLuongNhap = 0;
            var sumChiPhiNhap = 0;
            foreach (var list in bcns)
            {
                sumSoLuongNhap += list.SoLuongNhap;
                sumChiPhiNhap += list.ChiPhiNhap;
            }

            ViewBag.sumSoLuongNhap = sumSoLuongNhap;
            ViewBag.sumChiPhiNhap = sumChiPhiNhap;
            return View(bcns.ToList());
        }


        [HttpPost]
        [ValidateInput(false)]
        public EmptyResult Export(string GridHtml)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Grid.doc");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-word";
            Response.Output.Write(GridHtml);
            Response.Flush();   
            Response.End();
            return new EmptyResult();
        }

        public class BC
        {
            public string TenSP { get; set; }
            public int SoLuongThuc { get; set; }
            public int DoanhThu { get; set; }
            public int GiamGia { get; set; }
            public int DoanhThuThuc { get; set; }
            public int TongDoanhThu { get; set; }
        }

        public class BCN
        {
            public string TenSP { get; set; }
            public int SoLuongNhap { get; set; }
            public int GiaNhap { get; set; }
            public DateTime NgayNhap { get; set; }
            public int ChiPhiNhap { get; set; }
        }

        public class BCTheoDanhMuc
        {
            public string LSP { get; set; }
            public int SoLuongThuc { get; set; }
            public int DoanhThu { get; set; }
            public int GiamGia { get; set; }
            public int DoanhThuThuc { get; set; }
            public int TongDoanhThu { get; set; }
        }

        public class BCNCategory
        {
            public string LSP { get; set; }
            public int SoLuongNhap { get; set; }
            public int ChiPhiNhap { get; set; }
        }
    }
}