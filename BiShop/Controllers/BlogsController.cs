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
        // GET: Blogs
        public ActionResult Index(int? id,  int? page, int pageSize = 9)
        {
            var dsnews = Blogs.GetAll();
            if (page == null) page = 1;
            if (id != null)
            {
                dsnews = Blogs.GetTinTucByMaLoaiTin(id);
            }    
            
            int pageNumber = (page ?? 1);
            return View(dsnews.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult BlogDetail(int id)
        {
            var detail = Blogs.tinTucDetail(id);
            return View(detail);
        }
    }
}