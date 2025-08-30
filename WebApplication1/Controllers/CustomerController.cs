using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomer _customer;


        public CustomerController(ICustomer customer)
        {
            _customer = customer;
        }


        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            var result = _customer.GetAllCustomers();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCustomerByCustomerID(string CustomerID)
        {
            var result = _customer.GetCustomerByCustomerID(CustomerID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddCustomerDetails(GetCustomerModal addCustomer)
        {
            var result = _customer.AddCustomerDetails(addCustomer);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult PutCustomerDetails(GetCustomerModal addCustomer)
        {
            var result = _customer.PutCustomerDetails(addCustomer);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult DeleteCustomerDetails(GetCustomerModal addCustomer)
        {
            var result = _customer.DeleteCustomerDetails(addCustomer);
            return Json(result, JsonRequestBehavior.AllowGet);

        }









        // GET: Customer
        public ActionResult Index()
        {

            return View();
        }



    }
}