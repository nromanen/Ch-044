using Model.DTO;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface ICategoryManager
    {
        CategoryDTO Get(int id, bool includeChildren = false);

        int Add(string name, int parent = -1);

        bool Delete(int id);

        bool Rename(int id, string name);

        bool ChangeParent(int id, int parent);
        bool ChangeOrderNo(int id, int orderno);

        List<CategoryDTO> GetAll();
    }
}