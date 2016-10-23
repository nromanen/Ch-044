using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface {
	public interface ICategoryManager
	{
		CategoryDTO Get(int id, bool includeChildren = false);
		int Add(string name, int parent = -1);
		bool Delete(int id);
		bool Rename(int id, string name);
		bool ChangeParent(int id, int parent);
	}
}
