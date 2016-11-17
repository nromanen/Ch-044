using Model.DTO;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IPropertyManager
    {
        void Delete(int id);

        PropertyDTO Get(int id);

        void Add(string Name, string Description, string Type, string Prefix, string Sufix,
            int Category_Id, string DefaultValue);

        void Update(int id, string Name, string Description, string Type, string Prefix, string Sufix,
            string DefaultValue, int Category_Id);

        List<PropertyDTO> GetAll();
    }
}