using Model.Product;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface ILaptopManager
    {
        IEnumerable<Laptop> GetAll();

        Laptop GetById(int id);
    }
}