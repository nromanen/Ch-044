﻿using Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class IndexViewDTO
    {
		public List<GoodViewDTO> GoodCollection;
		public List<Good> GoodList;
		public List<CategoryDTO> CategoryList;

	}
}