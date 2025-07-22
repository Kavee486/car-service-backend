using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class ServiceCategoryController : Controller
    {

        private readonly IServiceCategory _servicecategory;
         

        //DATest DATest = new DATest();

        public ServiceCategoryController(IServiceCategory servicecategory)
        {
            _servicecategory = servicecategory;
        }

        [HttpGet]
        public ActionResult GetAllServiceCategories()
        {
            var result = _servicecategory.GetAllServiceCategories();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetAllServiceCategoriesById(string id)
        {
            var result = _servicecategory.GetAllServiceCategoriesById(id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        // GET: ServiceCategory
        public ActionResult Index()
        {
            return View();
        }
    }
}