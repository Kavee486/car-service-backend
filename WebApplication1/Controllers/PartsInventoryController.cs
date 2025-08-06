using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PartsInventoryController : Controller
    {
        private readonly IPartsInventory _PartsInventory;


        

        public PartsInventoryController(IPartsInventory partsinventory)
        {
            _PartsInventory = partsinventory;
        }

       

        [HttpGet]
        public ActionResult getAllPartsInventory()
        {
            var result = _PartsInventory.getAllPartsInventory();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetPartsInventoryByPartID(string PartID)
        {
            var result = _PartsInventory.GetPartsInventoryByPartID(PartID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AddPartInventoryDetails(GetPartsInventoryModal addPartsInventory)
        {
            var result = _PartsInventory.AddPartInventoryDetails(addPartsInventory);
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult PutPartInventoryDetails(GetPartsInventoryModal addPartsInventory)
        {
            var result = _PartsInventory.PutPartInventoryDetails(addPartsInventory);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpDelete]
        public ActionResult DeletePartInventoryDetails(GetPartsInventoryModal addPartsInventory)
        {
            var result = _PartsInventory.DeletePartInventoryDetails(addPartsInventory);
            return Json(result, JsonRequestBehavior.AllowGet);

        }



    }
}

