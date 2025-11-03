using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ICategory
    {


        Response GetAllCategories();
        Response PutCategoriesDetails(GetCategoryModal addCategory);
        Response AddCategoriesDetails(GetCategoryModal addCategory);
        Response DeleteCategoriesDetails(GetCategoryModal addCategory);

    }
}