using DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class LanguageController : Controller
    {
  
        public ActionResult Change(string languageSelect,string actionn=null,string seolink=null)
        {
            if (actionn == "Content2")
            {
                DatabaseContext db = new DatabaseContext();
                var artic = (from link in db.ArticleLangs where link.SeoLink == seolink select link.Article.Id).FirstOrDefault();
                var seo = (from link2 in db.ArticleLangs where link2.Lang.Slug == languageSelect & link2.Article.Id == artic select link2.SeoLink).FirstOrDefault();
                HttpCookie cookie2 = new HttpCookie("Language");
                cookie2.Value = languageSelect;
                Response.Cookies.Add(cookie2);
                ViewBag.Language = languageSelect;
                if (seo == null)
                    seo = seolink;
                return RedirectToRoute("SeoPost", new { seolink = seo });
               
            }
            

            if (languageSelect != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageSelect);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageSelect);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = languageSelect;
            Response.Cookies.Add(cookie);
            //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            //string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            string action = System.Web.HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("action");
            ViewBag.Language = languageSelect;
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Index()
        {
            return View();
        }
      
       
    }
}