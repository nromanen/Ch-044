using Model.DB;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class IndexViewDTO
    {
		
		public IPagedList<GoodDTO> GoodList;
		public List<CategoryDTO> CategoryList;
	}
}
