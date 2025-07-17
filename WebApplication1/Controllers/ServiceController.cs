using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IService _service;

        //DATest DATest = new DATest();

        public ServiceController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetAllServices()
        {
            var result = _service.GetAllServices();
            return Json(result, JsonRequestBehavior.AllowGet);
        }





        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
    }
}