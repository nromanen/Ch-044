﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace BAL.Interface
{
    public interface IPropertyManager
    {
        //  List<PropertyDTO> GetAll();
        void Delete(int id);

        void Add(string Name, string Description, string Type, string Prefix, string Sufix,
            int Characteristic_Id, int Category_Id, string DefaultValue);

        //   PropertyDTO Get(int id);
    }
}
