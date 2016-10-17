using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByEntity.Model
{
    public class Producer
    {
        public Producer()
        {
            Goods = new HashSet<Good>();
        }
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string Name { get; set; }

        public string Country { get; set; }

        public virtual ICollection<Good> Goods { get; set; }

    }
}
