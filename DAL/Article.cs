using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Article
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Clicks { get; set; }
        public virtual ICollection<Category>  Categories { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<ArticleLang> ArticleLangs { get; set; }
    }
}
