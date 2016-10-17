using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ByEntity.Model
{
    public class Good
    {        
        public int Id { get; set; }
               

        [Required, MaxLength(70)]
        public string Name { get; set; }

            
        public decimal Price { get; set; }

     
        public int CategoryId { get; set; }
        [Required]        
        public virtual Category Category { get; set; }


 
        public int ProducerId { get; set; } 
        [Required]
        public virtual Producer Producer { get; set; }


        //public override string ToString()
        //{
        //    return Name + " " + Price + " Cat_Id " + CategoryId + Category.ToString();
        //}
    }
}
