using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Interface;
using Common.Enum;
using DAL.Interface;
using log4net;
using Model.DB;
using Model.DTO;

namespace BAL.Manager
{
    public class PropertyManager : BaseManager, IPropertyManager
    {
        public PropertyManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        public void Delete(int id)
        {
            try
            {
                var property = uOW.PropertyRepo.GetByID(id);
                uOW.PropertyRepo.Delete(property);
                uOW.Save();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void Add(string Name, string Description, string Type, string Prefix, string Sufix, int Characteristic_Id, int Category_Id, string DefaultValue)
        {
            try
            {
                var newProperty = new Property() { Name = Name, Description = Description, Type = (PropertyType)Enum.Parse(typeof(PropertyType), Type), Prefix = Prefix, Sufix = Sufix, Characteristic_Id = Characteristic_Id, DefaultValue = DefaultValue, Category_Id = Category_Id };
                uOW.PropertyRepo.Insert(newProperty);
                uOW.Save();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void Update(int id, string Name, string Description, string Type, string Prefix, string Sufix,
            string DefaultValue)
        {
            try
            {
                var property = uOW.PropertyRepo.GetByID(id);
                property.Name = Name;
                property.Description = Description;
                property.Type = (PropertyType)Enum.Parse(typeof(PropertyType), Type);
                property.Prefix = Prefix;
                property.Sufix = Sufix;
                property.DefaultValue = DefaultValue;
                uOW.Save();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
