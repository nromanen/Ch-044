using AutoMapper;
using BAL.Interface;
using Common.Enum;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Manager
{
    public class PropertyManager : BaseManager, IPropertyManager
    {
        public PropertyManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        public PropertyDTO Get(int id)
        {
            try
            {
                var property = uOW.PropertyRepo.GetByID(id);
                if (property == null)
                {
                    return null;
                }

                var resultProperty = Mapper.Map<PropertyDTO>(property);
                return resultProperty;
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                return null;
            }

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

        public void Add(string Name, string Description, string Type, string Prefix, string Sufix, int Category_Id, string DefaultValue)
        {
            try
            {
                var newProperty = new Property() { Name = Name, Description = Description, Type = (PropertyType)Enum.Parse(typeof(PropertyType), Type), Prefix = Prefix, Sufix = Sufix, DefaultValue = DefaultValue, Category_Id = Category_Id };
                uOW.PropertyRepo.Insert(newProperty);
                uOW.Save();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void Update(int id, string Name, string Description, string Type, string Prefix, string Sufix,
            string DefaultValue, int Category_Id)
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
                property.Category_Id = Category_Id;
                uOW.Save();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public List<PropertyDTO> GetAll()
        {
            List<PropertyDTO> properties = new List<PropertyDTO>();
            foreach (var property in uOW.PropertyRepo.All.ToList())
            {
                var prop = uOW.PropertyRepo.GetByID(property.Id);
                properties.Add(Mapper.Map<PropertyDTO>(prop));
            }

            return properties;
        }
    }
}