using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class JobCardItemsController : Controller
    {
        private readonly IJobCardItems _JobCardItems;
        //private object _servicecategory;

        //DATest DATest = new DATest();

        public JobCardItemsController(IJobCardItems JobCardItems)
        {
            _JobCardItems = JobCardItems;
        }

        [HttpGet]
        public ActionResult GetAllJobCardItems()
        {
            var result = _JobCardItems.GetAllJobCardItems();
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetJobCardItemsByJobCardID(string JobCardID)
        {
            var result = _JobCardItems.GetJobCardItemsByJobCardID(JobCardID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetJobCardItemsByServiceID(string ServiceID)
        {
            var result = _JobCardItems.GetJobCardItemsByServiceID(ServiceID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult AddJobCardItemsDetails(GetJobCardItemsModal addJobCardItems)
        {
            var result = _JobCardItems.AddJobCardItemsDetails(addJobCardItems);
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult PutJobCardItemsDetails(GetJobCardItemsModal addJobCardItems)
        {
            var result = _JobCardItems.PutJobCardItemsDetails(addJobCardItems);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult DeletJobCardItemsDetails(GetJobCardItemsModal addJobCardItems)
        {
            var result = _JobCardItems.DeletJobCardItemsDetails(addJobCardItems);
            return Json(result, JsonRequestBehavior.AllowGet);

        }





        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }
    }
}