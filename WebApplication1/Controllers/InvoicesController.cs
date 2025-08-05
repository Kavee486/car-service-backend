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
    public class InvoicesController : Controller
    {
        private readonly IInvoices _Invoices;


        public InvoicesController(IInvoices Invoices)
        {
            _Invoices = Invoices;
        }



        [HttpGet]
        public ActionResult GetAllInvoices()
        {
            var result = _Invoices.GetAllInvoices();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetInvoicesByInvoiceID(string InvoiceID)
        {
            var result = _Invoices.GetInvoicesByInvoiceID(InvoiceID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult PutInvoicesDetails(GetInvoicesModal addInvoice)
        {
            var result = _Invoices.PutInvoicesDetails(addInvoice);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpDelete]
        public ActionResult DeleteInvoicesDetails(GetInvoicesModal addInvoice)
        {
            var result = _Invoices.DeleteInvoicesDetails(addInvoice);
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        // GET: Invoices
        public ActionResult Index()
        {
            return View();
        }
    }
}