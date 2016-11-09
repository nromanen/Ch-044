using Model.Product;
using System.Collections.Generic;

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