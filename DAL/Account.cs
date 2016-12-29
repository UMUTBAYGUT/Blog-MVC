using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Account
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public double Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public virtual ICollection<Article>  Articles { get; set; }

    }
}
