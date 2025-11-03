using System.Web.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.DataAccess;  // ? Add this for DAPreviousServices

namespace WebApplication1.Controllers
{
    public class PreviousServicesController : Controller
    {
        private readonly IPreviousServices _previousServices;

        // ? Add a parameterless constructor
        public PreviousServicesController()
        {
            _previousServices = new DAPreviousServices();  // Manual instance
        }

        [HttpGet]
        public ActionResult GetAllPreviousServices()
        {
            var result = _previousServices.GetAllPreviousServices();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPreviousServicesByCustomerID(string CustomerID)
        {
            var result = _previousServices.GetPreviousServicesByCustomerID(CustomerID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddPreviousService(GetPreviousServicesModal previousService)
        {
            var result = _previousServices.AddPreviousService(previousService);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult DeletePreviousService(string ServiceID)
        {
            var result = _previousServices.DeletePreviousService(ServiceID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
