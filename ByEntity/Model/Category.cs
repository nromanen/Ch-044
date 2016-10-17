using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //public override bool Equals(object obj)
        //{
        //    if (obj is Category)
        //    {
        //        Category curr = (Category)obj;
        //        curr.Goods = Goods;
        //        curr.Name = Name;
        //    }
             
        //}
    }
}
