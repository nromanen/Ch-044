using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class GoodViewModelDTO
    {
        public GoodDTO Good { get; set; }

        public List<GoodDTO> AllOffers { get; set; }

        public List<GoodDTO> SimilarOffers { get; set;}

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
