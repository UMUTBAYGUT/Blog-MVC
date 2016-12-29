using System.Web.Mvc;

namespace Blog.Areas.Admin
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

            context.MapRoute("AdminBase", "Admin", new { controller = "Security", action = "Index" });

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { id = UrlParameter.Optional }
            );

            context.MapRoute("AdminCategory", "Admin/Category/Index", new { controller = "Security", action = "Index" });
        }
    }
}