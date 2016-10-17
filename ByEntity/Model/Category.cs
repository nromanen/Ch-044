using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ByEntity.Model
{
    public class Category
    {
        public Category()
        {
            Goods = new HashSet<Good>();
           
        }
       
        public int Id { get; set; }

        
        [Required, MaxLength(50)]
        public string Name { get; set; }

       
        public virtual ICollection<Good> Goods { get; set; }

        public override string ToString()
        {
            return Id + " " + Name;
        }

        

    }
}
