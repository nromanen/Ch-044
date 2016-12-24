using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
	public class GetCategoryDTO
	{
		public IPagedList<GoodDTO> GoodListCategory;
		public string CategoryName;

	}
}
