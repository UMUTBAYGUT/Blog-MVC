using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryLang
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }
        public virtual Category Category { get; set; }
        public virtual Lang Lang { get; set; }

    }
}
