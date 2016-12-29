using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace Blog.Binding
{
    public class CategoryBinding : IModelBinder

    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            using (DatabaseContext db = new DatabaseContext())
                {
                HttpRequestBase istek = controllerContext.HttpContext.Request;
                    if (istek.Form.Get("parent") != "NULL")
                     {
                    Category category= new Category();
                    var parentCategory = db.Categories.FirstOrDefault(ct => ct.Id == Int32.Parse(istek.Form.Get("parent")));
                    category.ParentCategory = parentCategory;
                    if (istek.Form.Get("active") == "active")
                    {
                        category.IsActive = true;
                    }
                    else
                    {
                        category.IsActive = false;
                    }
                    //category.CategoryLangs = new List<CategoryLang>();
                    var langs = db.Langs.ToList();
                      foreach (var lang in langs)
                         {
                        // CategoryLang _catlang= new CategoryLang();
                        //_catlang.Lang = lang;
                        //_catlang.Name = istek.Form.Get($"name_{lang.Id}");
                        //_catlang.Slug = istek.Form.Get($"slug_{lang.Id}");
                         category.CategoryLangs.Add(new CategoryLang()
                         {
                             Lang = lang,
                             Name =  istek.Form.Get($"name_{lang.Id}"),
                             Slug=  istek.Form.Get($"slug_{lang.Id}")
                         });
                        }
                      return category;
                    }
                     else
                    {
                        Category category = new Category();
                        category.ParentCategory = null;
                        if (istek.Form.Get("active") == "active")
                        {
                            category.IsActive = true;
                        }
                        else
                        {
                            category.IsActive = false;
                        }
                        
                        category.CategoryLangs = new List<CategoryLang>();
                        var langs = db.Langs.ToList();
                    
                         foreach (var lang in langs)
                         {
                        //var catlang = new CategoryLang
                        //{
                        //    Lang = lang,
                        //    Name = istek.Form.Get($"name_{lang.Id}"),
                        //    Slug = istek.Form.Get($"slug_{lang.Id}")
                        //};
                        category.CategoryLangs.Add(new CategoryLang()
                        {
                            Lang = lang,
                            Name = istek.Form.Get($"name_{lang.Id}"),
                            Slug = istek.Form.Get($"slug_{lang.Id}")
                        });

                    }
                        return category;
                }
            }
            

        }
    }
}