 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public  class ArticleLang
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string SeoLink { get; set; }
        public virtual Article Article { get; set; }
        public virtual  Lang Lang { get; set; }
        

    }
}
