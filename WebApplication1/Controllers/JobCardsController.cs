using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class JobCardsController : Controller
    {
        private readonly IJobCards _JobCards;


        //private readonly IJobCards _JobCards;

        public JobCardsController(IJobCards JobCards)
        {
            _JobCards = JobCards;
        }



        [HttpGet]
        public ActionResult GetAllJobCards()
        {
            var result = _JobCards.GetAllJobCards();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetJobCardsByJobCardID(string JobCardID)
        {
            var result = _JobCards.GetJobCardsByJobCardID(JobCardID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }









        // GET: JobCards
        public ActionResult Index()
        {
            return View();
        }
    }
}