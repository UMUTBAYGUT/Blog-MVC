using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace Blog.Areas.Admin.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public IEnumerable<Lang> Languages { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<CategoryLang> CategoryLangs { get; set; }
    }
}