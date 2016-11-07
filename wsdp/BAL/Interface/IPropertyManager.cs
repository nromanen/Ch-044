using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace BAL.Interface
{
    public interface IPropertyManager
    {
        void Delete(int id);

        void Add(string Name, string Description, string Type, string Prefix, string Sufix,
            int Category_Id, string DefaultValue);

        void Update(int id, string Name, string Description, string Type, string Prefix, string Sufix,
            string DefaultValue, int Category_Id);

        List<PropertyDTO> GetAll();
    }
}
