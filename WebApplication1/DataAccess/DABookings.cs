using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Database_Layer;

namespace WebApplication1.DataAccess
{
    public class DABookings: IBookings
    {
        public Response GetAllBookings()

        {
            Response res = new Response();
            List<GetBookingsModal> BookingsList = new List<GetBookingsModal>();

            string Query = "SELECT " +
                                "BookingID, " +
                                "CustomerID," +
                                "VehicleID, " +
                                "BookingDate," +
                                "PrefferedDate, " +
                                "Status " +

                            "FROM " +
                                "Bookings ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetBookingsModal Bookings = new GetBookingsModal
                        {
                            B_BookingID = reader["BookingID"].ToString(),
                            B_CustomerID = reader["CustomerID"].ToString(),
                            B_VehicleID = reader["VehicleID"].ToString(),
                            B_BookingDate = reader["BookingDate"].ToString(),
                            B_PrefferedDate = reader["PrefferedDate"].ToString(),
                            B_Status = reader["Status"].ToString()
                        };


                        BookingsList.Add(Bookings);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = BookingsList;
            return res;


        }

        public Response GetBookingsByCustomerID(string CustomerID)
        {


            Response res = new Response();
            List<GetBookingsModal> BookingsList = new List<GetBookingsModal>();

            string Query = "SELECT " +
                                "BookingID, " +
                                "CustomerID," +
                                "VehicleID, " +
                                "BookingDate," +
                                "PrefferedDate, " +
                                "Status " +
                            "FROM " +
                                     "Bookings " +
                            "WHERE " +
                                     "CustomerID = '" + CustomerID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetBookingsModal Bookings = new GetBookingsModal
                        {
                            B_BookingID = reader["BookingID"].ToString(),
                            B_CustomerID = reader["CustomerID"].ToString(),
                            B_VehicleID = reader["VehicleID"].ToString(),
                            B_BookingDate = reader["BookingDate"].ToString(),
                            B_PrefferedDate = reader["PrefferedDate"].ToString(),
                            B_Status = reader["Status"].ToString()
                        };
                        BookingsList.Add(Bookings);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = BookingsList;
            return res;
        }

        public Response GetBookingsByVehicleID(string VehicleID)
        {
            Response res = new Response();
            List<GetBookingsModal> BookingsList = new List<GetBookingsModal>();

            string Query = "SELECT " +
                                "BookingID, " +
                                "CustomerID," +
                                "VehicleID, " +
                                "BookingDate," +
                                "PrefferedDate, " +
                                "Status " +
                            "FROM " +
                                     "Bookings " +
                            "WHERE " +
                                     "VehicleID = '" + VehicleID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetBookingsModal Bookings = new GetBookingsModal
                        {
                            B_BookingID = reader["BookingID"].ToString(),
                            B_CustomerID = reader["CustomerID"].ToString(),
                            B_VehicleID = reader["VehicleID"].ToString(),
                            B_BookingDate = reader["BookingDate"].ToString(),
                            B_PrefferedDate = reader["PrefferedDate"].ToString(),
                            B_Status = reader["Status"].ToString()
                        };
                        BookingsList.Add(Bookings);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = BookingsList;
            return res;
        }
    }
}