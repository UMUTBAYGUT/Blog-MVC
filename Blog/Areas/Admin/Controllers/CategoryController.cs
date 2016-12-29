using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Areas.Admin.Models;
using Blog.Binding;
using DAL;

namespace Blog.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Add()
        {
            using (DatabaseContext db= new DatabaseContext())
            {
                CategoryModel model = new CategoryModel();
                model.Languages = db.Langs.ToList();
                model.Categories = db.Categories.ToList();
                ViewBag.Category = db.CategoryLangs.Where(cd => cd.Lang.Slug== "tr").ToList();  
                  
            return View(model);

            }
        }
       
        [HttpPost]
        public ActionResult Add([ModelBinder(typeof(CategoryBinding))] Category model)
        {
            using (DatabaseContext db2 = new DatabaseContext())
            {
                db2.Categories.Add(model);
                db2.SaveChanges();
            }
            
            return RedirectToAction("Add");
        }
    }
}