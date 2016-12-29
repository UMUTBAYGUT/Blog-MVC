using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

using System.Web.Routing;
using System.Threading;
using System.Globalization;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Homessss
        public ActionResult Index()
        {
            DatabaseContext db = new DatabaseContext();
            Lang lang = new Lang();

            HttpCookie cookieLang = System.Web.HttpContext.Current.Request.Cookies["Language"];
            if (cookieLang != null && cookieLang.Value != null)
            {
                lang = (from lg in db.Langs where lg.Slug == cookieLang.Value select lg).FirstOrDefault();
            }
            else
            {
                lang = db.Langs.FirstOrDefault();
            }

            
            var result = (from article in db.ArticleLangs where article.Lang.Id == lang.Id select article).ToList();
            @ViewBag.Language = lang.Slug;
            return View(result);
        }
        public ActionResult Content(int id)
        {

            DatabaseContext db = new DatabaseContext();
            Lang lang = new Lang();

            HttpCookie cookieLang = System.Web.HttpContext.Current.Request.Cookies["Language"];
            if (cookieLang != null && cookieLang.Value != null)
            {
                lang = (from lg in db.Langs where lg.Slug == cookieLang.Value select lg).FirstOrDefault();
            }
            else
            {
                lang = db.Langs.FirstOrDefault();
            }
            
            var result = db.ArticleLangs.Where(post => post.Article.Id == id && post.Lang.Id==lang.Id).FirstOrDefault();

            return View(result);
        }

        public  ActionResult Content2(string seolink)
        {

            DatabaseContext db = new DatabaseContext();
            Lang lang = new Lang();
          
               
                lang = (from lg in db.ArticleLangs where lg.SeoLink == seolink select lg.Lang).FirstOrDefault();

                if (lang != null)
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang.Slug);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang.Slug);
                    ViewBag.Language = lang.Slug;
                }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            if (Request.Cookies["Language"] != null)
            {
                Request.Cookies["Language"].Value = lang.Slug;
                ViewBag.Language = lang.Slug;

            }
            else
            {
                HttpCookie cookie = new HttpCookie("language");
                cookie.Value = lang.Slug;
                Response.Cookies.Add(cookie);
                ViewBag.Language = lang.Slug;
            }
               


           

            var result = db.ArticleLangs.Where(post => post.SeoLink == seolink && post.Lang.Id == lang.Id).FirstOrDefault();
            //ViewBag.UrlSeolink = (from url in result.Article.ArticleLangs where url.Lang.Id != lang.Id select url.SeoLink).FirstOrDefault();
            //var langs = (from lang3 in db.Langs where lang3.Id != lang.Id select lang3).ToList() ;
            //foreach(var lan in langs)
            //{
            //    ViewData[lan.Slug]= (from url in result.Article.ArticleLangs where url.Lang.Id != lang.Id select url.SeoLink).FirstOrDefault();
            //}
            if (result == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Home/Content.cshtml",result);
        }

        public ActionResult Menu()
        {
            DatabaseContext db = new DatabaseContext();
         
            if (Request.Cookies["Language"] != null)
            {
                var cookie = Request.Cookies["Language"].Value.ToString();
                var resutl = (from lg in db.Langs where lg.Slug != cookie select lg).ToList();
                return PartialView("~/Views/Shared/_Menu.cshtml", resutl);
            }
            else
            {
                var resutl = db.Langs.ToList();
                return PartialView("~/Views/Shared/_Menu.cshtml", resutl);
            }
                                  
        }
    }
}