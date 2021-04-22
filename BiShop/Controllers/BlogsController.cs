using BiShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiShop.Controllers
{
    public class BlogsController : Controller
    {
        DbBiLuxuryEntities data = new DbBiLuxuryEntities();
        // GET: Blogs
        public ActionResult Index(int? id,  int? page, int pageSize = 9)
        {
            var dsnews = data.TinTucs.ToList(); ;
            if (page == null) page = 1;
            if (id != null)
            {
                dsnews = data.TinTucs.Where(x => x.MaLoaiTin == id).ToList();
            }    
            
            int pageNumber = (page ?? 1);
            return View(dsnews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult BlogDetail(int id)
        {
            var detail = data.TinTucs.FirstOrDefault(x => x.Id == id);
            return View(detail);
        }
    }
}