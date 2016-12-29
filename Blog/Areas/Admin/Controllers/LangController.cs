using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Windows.Forms.VisualStyles;
using Blog.Areas.Admin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blog.Areas.Admin.Controllers
{
    public class LangController : Controller
    {
        // GET: Admin/Language
        public ActionResult Index()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var result = db.Langs.ToList();
                return View(result);
            }
           
        }

        public ActionResult LangItem(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<ValueResxModel> model = new List<ValueResxModel>();

                 Lang lang = db.Langs.FirstOrDefault(lg => lg.Id == id);
              

                using (System.IO.StreamReader _StreamReader = new System.IO.StreamReader(Server.MapPath($"/Language/lang/{lang.Slug}.json")))
                {
                    string jsonData = _StreamReader.ReadToEnd();
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
                    foreach (var value in values)
                    {
                        model.Add(new ValueResxModel
                        {
                            Key = value.Key,
                            Value = value.Value
                        });
                    }
                }


                ViewBag.LangSlug = lang.Slug;

                return View(model);
            }

        }

        [HttpPost]
        public ActionResult LangItem(string veri,string slug =null)
        {
            string modifiedJsonString;
            using (StreamReader _StreamReader = new StreamReader(Server.MapPath($"/Language/lang/{slug}.json")))
            {
                string jsonData = _StreamReader.ReadToEnd();

                var list = JsonConvert.DeserializeObject<List<ValueResxModel2>>(veri);
                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(jsonData);
                foreach (var ls in list)
                {
                    jsonObj.Property(ls.name).Value = ls.value;
                }
                
                 modifiedJsonString = JsonConvert.SerializeObject(jsonObj);
                
            }
            using (StreamWriter _StreamWriter = new StreamWriter(Server.MapPath($"/Language/lang/{slug}.json")))
            {
                _StreamWriter.WriteLine(modifiedJsonString);
            }


            return RedirectToAction("Index");
        }
        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Add(Lang lang)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
              
                   
                    //   Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    using (StreamReader _StreamReader=new StreamReader(Server.MapPath($"/Language/lang/en.json")))
                    {
                        string jsonData = _StreamReader.ReadToEnd();
                        System.IO.File.WriteAllText(Server.MapPath($"/Language/lang/{lang.Slug}.json"), jsonData);
                      
                    }
                

                    db.Langs.Add(lang);
                    db.SaveChanges();
                
            }
            return RedirectToAction("Add");
        }



        /*
        public ActionResult Add()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Add(Lang lang)
        {
            using (DatabaseContext db = new DatabaseContext())
            {


                using (
                    ResXResourceWriter resx =
                        new ResXResourceWriter(Server.MapPath($"/Resources/front.{lang.Slug}.resx")))
                {
                    using (
                        ResXResourceReader resxReader = new ResXResourceReader(Server.MapPath($"/Resources/front.resx"))
                    )
                    {
                        foreach (DictionaryEntry entry in resxReader)
                        {
                            resx.AddResource(entry.Key.ToString(), entry.Value.ToString());
                        }
                    }
                    resx.Generate();
                    resx.Close();
                    db.Langs.Add(lang);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Add");
        }

        public ActionResult Delete(int id)
        {
            using (DatabaseContext db= new DatabaseContext())
            {
                var result = db.Langs.Where(lg => lg.Id == id).FirstOrDefault();
                return View(result);
            }
            
        }

        [HttpPost]
        public ActionResult Delete(Lang lang)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                lang = db.Langs.Where(lg => lg.Id == lang.Id).FirstOrDefault();
                if (System.IO.File.Exists(Server.MapPath($"/Resources/front.{lang.Slug}.resx")))
                {
                    System.IO.File.Delete(Server.MapPath($"/Resources/front.{lang.Slug}.resx"));
                }
                if (System.IO.File.Exists(Server.MapPath($"/Resources/front.{lang.Slug}.Designer.cs")))
                {
                    System.IO.File.Delete(Server.MapPath($"/Resources/front.{lang.Slug}.Designer.cs"));
                }

                    
                db.Langs.Remove(lang);
                db.SaveChanges();
                if (Request.Cookies["Language"] != null)
                    if (Request.Cookies["Language"].Value==lang.Slug)
                     {
                    HttpCookie myCookie = new HttpCookie("Language");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                      }
                return RedirectToAction("Index");
            }

        }

        public ActionResult LangItem2(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                ValueResxModel values= new ValueResxModel();
                
                Lang lang = db.Langs.Where(lg => lg.Id == id).FirstOrDefault();
                ResXResourceReader resxReader =
                    new ResXResourceReader(Server.MapPath($"/Resources/front.{lang.Slug}.resx"));

                values.Resource = resxReader.OfType<DictionaryEntry>();
                return View(values);
            }
            
        }

        public ActionResult LangItem(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
               List<ValueResxModel2> test= new List<ValueResxModel2>();

                Lang lang = db.Langs.Where(lg => lg.Id == id).FirstOrDefault();
                ResXResourceReader resxReader =
                    new ResXResourceReader(Server.MapPath($"/Resources/front.{lang.Slug}.resx"));
                
                foreach (var a in resxReader.OfType<DictionaryEntry>())
                {
                   test.Add( new ValueResxModel2()
                    {
                       Key = a.Key.ToString(),
                       Value = a.Value.ToString()
                    });
                }

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(test);
                return View(test);
            }
         
        }

        [HttpPost]
        public ActionResult LangItem(string veri)
        {
            Hashtable h = new Hashtable();
            var list = JsonConvert.DeserializeObject<List<ValueResxModel3>>(veri);
            foreach (var _list in list)
            {
                
                h.Add(_list.name,_list.value);
               
            }

            UpdateResourceFile(h, Server.MapPath($"/Resources/front.tr.resx"));
            return RedirectToAction("Index");
        }


        [HttpPost]
        public PartialViewResult Edit(string slug,string key,string value)
        {


            return PartialView();
        }
        public static void UpdateResourceFile(Hashtable data, String path)
        {
            Hashtable resourceEntries = new Hashtable();

            //Get existing resources
            ResXResourceReader reader = new ResXResourceReader(path);
            if (reader != null)
            {
                IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    if (d.Value == null)
                        resourceEntries.Add(d.Key.ToString(), "");
                    else
                        resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                }
                reader.Close();
            }

            //Modify resources here...
            foreach (String key in data.Keys)
            {
                if (resourceEntries.ContainsKey(key))
                {

                    String value = data[key].ToString();
                    if (value == null) value = "";

                    resourceEntries.Add(key, value);
                }
            }

            //Write the combined resource file
            ResXResourceWriter resourceWriter = new ResXResourceWriter(path);

            foreach (String key in resourceEntries.Keys)
            {
                resourceWriter.AddResource(key, resourceEntries[key]);
            }
            resourceWriter.Generate();
            resourceWriter.Close();

        }*/

    }

    
}