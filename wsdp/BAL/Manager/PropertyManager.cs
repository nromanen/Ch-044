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
        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
        public PropertyManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        //public List<PropertyDTO> GetAll()
        //{
        //    //return uOW.PropertyRepo.All();
        //}


        public void Delete(int id)
        {
            uOW.PropertyRepo.Delete(id);
            uOW.Save();

        }

        public void Add(string Name, string Description, string Type, string Prefix, string Sufix, int Characteristic_Id, int Category_Id, string DefaultValue)
        {
            var newProperty = new Property() { Name = Name, Description = Description, Type = (PropertyType)Enum.Parse(typeof(PropertyType), Type), Prefix = Prefix, Sufix = Sufix, Characteristic_Id = Characteristic_Id, DefaultValue = DefaultValue, Category_Id = Category_Id };
            uOW.PropertyRepo.Insert(newProperty);
            uOW.Save();
        }

        //public PropertyDTO Get(int id)
        //{
        //    var property = uOW.CategoryRepo.GetByID(id);
        //    if (property == null) return null;
        //    var result = Mapper.Map<PropertyDTO>(property);
        //    result = new List<CategoryDTO>();

        //    if (includeChildren && category.ChildrenCategory != null)
        //    {
        //        foreach (var child in category.ChildrenCategory)
        //        {
        //            result.ChildrenCategory.Add(Get(child.Id, true));
        //        }
        //    }

        //    return result;
        //}
    }
}
