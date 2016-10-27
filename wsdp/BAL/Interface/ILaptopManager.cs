using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Product;

namespace BAL.Interface
{
    public interface ILaptopManager
    {
        IEnumerable<Laptop> GetAll();
        Laptop GetById(int id);

    }
}
