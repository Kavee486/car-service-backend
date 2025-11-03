using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategory _categoryService;  // Assuming ICategory is your service interface for Category

        // Constructor for dependency injection
        public CategoryController(ICategory categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        // GET: GetAllCategories
        [HttpGet]
        public ActionResult GetAllCategories()
        {
            var result = _categoryService.GetAllCategories();
            return Json(result, JsonRequestBehavior.AllowGet);  // Returning as JSON for an API response
        }



        // POST: AddCategoryDetails
        [HttpPost]
        public ActionResult AddCategoriesDetails(GetCategoryModal addCategory)
        {
            var result = _categoryService.AddCategoriesDetails(addCategory);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // PUT: UpdateCategoryDetails
        [HttpPost]
        public ActionResult PutCategoriesDetails(GetCategoryModal updateCategory)
        {
            var result = _categoryService.PutCategoriesDetails(updateCategory);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult DeleteCategoriesDetails(GetCategoryModal addCategory)
        {
            var result = _categoryService.DeleteCategoriesDetails(addCategory);
            return Json(result, JsonRequestBehavior.AllowGet);

        }





    }
}