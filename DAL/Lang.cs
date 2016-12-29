using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Lang
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public string Slug { get; set; }
        public bool Default { get; set; }
        public virtual Account Account { get; set; }
      

    }
}
