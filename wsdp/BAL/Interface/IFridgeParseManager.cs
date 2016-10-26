using Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IFridgeParseManager
    {
        void GetConcreteGoodsFromCategory(string url);
        int GetCountOfPages(string url);
        List<Fridge> GetFridgesFromPage(string url);
        Fridge ParseFridge(string url);

    }
}
