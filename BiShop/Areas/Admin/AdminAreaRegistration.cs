using System.Web.Mvc;

namespace BiShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "Delete-Review",
                "Admin/{controller}/{action}/{id1}",
                 new { controller = "Reviews", action = "Delete", id1 = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );

            context.MapRoute(
               "CTDonHang",
               "Admin/{controller}/{action}/{id1}",
               new { controller = "CTDonHangs", action = "Edit", id1 = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );

            context.MapRoute(
                "Search-info",
                "Admin/tim-kiem",
                new { controller = "AdminManagement", action = "SeachInfo", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_login",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "AdminManagement", action = "LoginAdmin", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { Controller = "AdminManagement", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}