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
    public class BookingsController : Controller
    {
        private readonly IBookings _Booking;


        public BookingsController(IBookings Booking)
        {
            _Booking = Booking;
        }



        [HttpGet]
        public ActionResult GetAllBookings()
        {
            var result = _Booking.GetAllBookings();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetBookingsByCustomerID(string CustomerID)
        {
            var result = _Booking.GetBookingsByCustomerID(CustomerID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetBookingsByVehicleID(string VehicleID)
        {
            var result = _Booking.GetBookingsByVehicleID(VehicleID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        [HttpGet]
        public ActionResult GetBookingsByBookingID(string BookingID)
        {
            var result = _Booking.GetBookingsByVehicleID(BookingID);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult PutBookingsDetails(GetBookingsModal addBookings)
        {
            var result = _Booking.PutBookingsDetails(addBookings);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpDelete]
        public ActionResult DeleteBookingsDetails(GetBookingsModal addBookings)
        {
            var result = _Booking.DeleteBookingsDetails(addBookings);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        // GET: Bookings
        public ActionResult Index()
        {
            return View();
        }
    }
}