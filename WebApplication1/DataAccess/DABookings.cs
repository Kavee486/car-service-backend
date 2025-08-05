using biZTrack.Static;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

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
                                "PreferredDate, " +
                                "BookingStatus " +

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
                            B_PreferredDate = reader["PreferredDate"].ToString(),
                            B_BookingStatus = reader["BookingStatus"].ToString()
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
                                "PreferredDate, " +
                                "BookingStatus " +
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
                            B_PreferredDate = reader["PreferredDate"].ToString(),
                            B_BookingStatus = reader["BookingStatus"].ToString()
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
                                "PreferredDate, " +
                                "BookingStatus " +
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
                            B_PreferredDate = reader["PreferredDate"].ToString(),
                            B_BookingStatus = reader["BookingStatus"].ToString()
                        };
                        BookingsList.Add(Bookings);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = BookingsList;
            return res;
        }




        public Response GetBookingsByBookingID(string BookingID)
        {
            Response res = new Response();
            List<GetBookingsModal> BookingsList = new List<GetBookingsModal>();

            string Query = "SELECT " +
                                "BookingID, " +
                                "CustomerID," +
                                "VehicleID, " +
                                "BookingDate," +
                                "PreferredDate, " +
                                "BookingStatus " +
                            "FROM " +
                                     "Bookings " +
                            "WHERE " +
                                     "BookingID = '" + BookingID + "'";
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
                            B_PreferredDate = reader["PreferredDate"].ToString(),
                            B_BookingStatus = reader["BookingStatus"].ToString()
                        };
                        BookingsList.Add(Bookings);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = BookingsList;
            return res;
        }

        public Response PutBookingsDetails(GetBookingsModal addBookings)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE Bookings
                                        SET BookingStatus = '" + addBookings.B_BookingStatus + @"'
                                        WHERE BookingID = '" + addBookings.B_BookingID + @"'";


                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))
                    {
                        res.StatusCode = 200;
                        res.Result = "Success!!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed!!";
            }
            return res;
        }



        public Response DeleteBookingsDetails(GetBookingsModal addBookings)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE Bookings
                                        SET Status = 'I'
                                        WHERE BookingID = '" + addBookings.B_BookingID + @"'";


                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))
                    {
                        res.StatusCode = 200;
                        res.Result = "Success!!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed!!";
            }
            return res;
        }
    }
}