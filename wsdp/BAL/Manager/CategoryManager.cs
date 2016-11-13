using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Manager
{
    public class CategoryManager : BaseManager, ICategoryManager
    {
        public CategoryManager(IUnitOfWork uOW) : base(uOW)
        {
        }

        /// <summary>
        /// Get Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeChildren"></param>
        /// <returns></returns>
        public CategoryDTO Get(int id, bool includeChildren = false)
        {
            var category = uOW.CategoryRepo.GetByID(id);
            if (category == null) return null;
            var result = Mapper.Map<CategoryDTO>(category);
            result.ChildrenCategory = new List<CategoryDTO>();

            var propertiesForResult = uOW.PropertyRepo.All.Where(prop => prop.Category_Id == result.Id).ToList();
            result.PropertiesList = Mapper.Map<List<PropertyDTO>>(propertiesForResult);

            if (includeChildren && category.ChildrenCategory != null)
            {
                foreach (var child in category.ChildrenCategory.OrderBy(x => x.OrderNo))
                {
                    result.ChildrenCategory.Add(Get(child.Id, true));
                }
            }

            return result;
        }

        /// <summary>
        /// Add Category to database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public int Add(string name, int parentId = -1)
        {
            var parent = parentId != -1 ? uOW.CategoryRepo.GetByID(parentId) : null;
            var newCategory = new Category() { Name = name, ParentCategory = parent };
            newCategory.OrderNo = uOW.CategoryRepo.All.ToList().Where(c => c.ParentCategory == parent).Max(x => x.OrderNo) + 1;
            uOW.CategoryRepo.Insert(newCategory);
            uOW.Save();
            return newCategory.Id;
        }

        /// <summary>
        /// Delete Category from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                var category = uOW.CategoryRepo.GetByID(id);
                if (category == null) return false;

                if (category.ChildrenCategory != null)
                {
                    foreach (var children in category.ChildrenCategory)
                    {
                        children.ParentCategory = category.ParentCategory;
                    }

                    uOW.Save();
                }

                //uOW.CategoryRepo.Delete(id);
                uOW.CategoryRepo.Delete(category);
                uOW.Save();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Rename Category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Rename(int id, string name)
        {
            try
            {
                var category = uOW.CategoryRepo.GetByID(id);
                if (category == null) return false;
                category.Name = name;
                uOW.Save();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Change Parent of Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public bool ChangeParent(int id, int parentId)
        {
            try
            {
                if (parentId == -1)
                {
                    uOW.CategoryRepo.GetByID(id).ParentCategory = null;
                    return true;
                }
                var parent = uOW.CategoryRepo.GetByID(parentId);
                uOW.CategoryRepo.GetByID(id).ParentCategory = parent;
                uOW.Save();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return false;
        }
        /// <summary>
        /// Changer OrderNo for one category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderno"></param>
        /// <returns></returns>
        public bool ChangeOrderNo(int id, int orderno)
        {
            try
            {
                var category = uOW.CategoryRepo.GetByID(id);
                if (category == null)
                    return false;

                category.OrderNo = orderno;
                uOW.Save();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns></returns>
        public List<CategoryDTO> GetAll()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            foreach (var category in uOW.CategoryRepo.All.ToList().OrderBy(x => x.OrderNo))
            {
                var categoryWithChildren = this.Get(category.Id, true);
                categories.Add(categoryWithChildren);
            }

            return categories;
        }
    }
}