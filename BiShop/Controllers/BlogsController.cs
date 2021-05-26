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
        private DbBiLuxuryEntities data = new DbBiLuxuryEntities();
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
            var detail = data.TinTucs.FirstOrDefault(x => x.Id == id);
            
            int UrlId = id;
            //check if the user opening the site for the first time 
            if (Session["URLHistory"] != null)
            {
                //The session variable exists. So the user has already visited this site and sessions is still alive. Check if this page is already visited by the user
                List<int> HistoryURLs = (List<int>)Session["URLHistory"];
                if (HistoryURLs.Exists((element => element == UrlId)))
                {
                    //If the user has already visited this page in this session, then we can ignore this visit. No need to update the counter.
                    Session["VisitedURL"] = 0;
                }
                else
                {
                    //if the user is visting this page for the first time in this session, then count this visit and also add this page to the list of visited pages(URLHistory variable)
                    HistoryURLs.Add(UrlId);
                    Session["URLHistory"] = HistoryURLs;
                    //Make a note of the page Id to update the database later 
                    Session["VisitedURL"] = UrlId;
                    detail.LuotXem = detail.LuotXem + 1;
                    
                }
            }
            else
            {
                //if there is no session variable already created, then the user is visiting this page for the first time in this session. Then create a session variable and take the count of the page Id
                List<int> HistoryURLs = new List<int>();
                HistoryURLs.Add(UrlId);
                Session["URLHistory"] = HistoryURLs;
                Session["VisitedURL"] = UrlId;
                detail.LuotXem = detail.LuotXem + 1;
            }
            data.TinTucs.Add(detail);
            data.SaveChanges();

            var listNew = data.TinTucs.Where(x => x.LoaiTin.Id == detail.LoaiTin.Id).Take(3).ToList();
            ViewBag.ListNew = listNew;

            return View(detail);
        }
    }
}