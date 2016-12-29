using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Category
    {
        public int Id { get; set; }
        /*ParentId eskiden int olarak tutuyordum . şimdi nesne olarak tutacağım
        public int ParentId { get; set; }*/
        public virtual Category ParentCategory { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Depth { get; set; }
        public bool IsActive { get; set; }
        public int Clicks { get; set; }
        public virtual ICollection<CategoryLang> CategoryLangs { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        

    }
}
