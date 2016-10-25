using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Helpers
{
    public static class CategoryHelper
    {
        public static MvcHtmlString CreateList(this HtmlHelper html, List<CategoryDTO> categories)
        {
            TagBuilder ol = new TagBuilder("ol");
            ol.AddCssClass("todo");
            ol.InnerHtml = "";
            foreach (var category in categories)
            {
                TagBuilder li = new TagBuilder("li");
                li.Attributes.Add("data-id", category.Id.ToString());
                li.Attributes.Add("data-name", category.Name);
                li.InnerHtml = category.Name;

                if (category.ChildrenCategory != null)
                {
                    li.InnerHtml += CreateList(html, category.ChildrenCategory.ToList());
                }
                ol.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(ol.ToString());
        }
    }
}