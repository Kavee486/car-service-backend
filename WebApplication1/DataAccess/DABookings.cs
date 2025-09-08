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
                // Update booking status
                string updateQuery = @"UPDATE Bookings
                               SET BookingStatus = '" + addBookings.B_BookingStatus + @"'
                               WHERE BookingID = '" + addBookings.B_BookingID + @"'";

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(updateQuery))
                    {
                        // Fetch customer's phone number for this booking
                        string getCustomerQuery = @"SELECT c.Phone
                                            FROM Customers c
                                            INNER JOIN Bookings b ON c.CustomerID = b.CustomerID
                                            WHERE b.BookingID = '" + addBookings.B_BookingID + @"'";

                        string customerPhone = dbConnect.ExecuteScalar(getCustomerQuery)?.ToString();

                        // Send SMS if phone is found
                        if (!string.IsNullOrEmpty(customerPhone))
                        {
                            string message = "Your AudoDeck vehicle Service booking status has been updated to: " + addBookings.B_BookingStatus;
                            SendSMS(customerPhone, message);
                        }

                        res.StatusCode = 200;
                        res.Result = "Success!! Booking updated and SMS sent.";
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

        // SMS sending function using your service
        private void SendSMS(string contact, string message)
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    string encodedMessage = System.Web.HttpUtility.UrlEncode(message);
                    string smsApiUrl = $"https://esystems.cdl.lk/Backend/SMSGateway/api/SMS/DTSSendMessage?mobileNo={contact}&message={encodedMessage}";
                    var response = client.GetAsync(smsApiUrl).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        LogHandler.WriteToLog($"SMS sending failed to {contact}", "SendSMS");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, "SendSMS");
            }
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

        public Response AddBookingsDetails(GetBookingsModal addBookings)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO Bookings " +
                                          "(CustomerID," +
                                           "VehicleID," +
                                           "BookingDate," +
                                           "PreferredDate," +
                                           "Status," +
                                           "BookingStatus) " +
                               "VALUES('" + addBookings.B_CustomerID + "'," +
                                       "'" + addBookings.B_VehicleID + "'," +
                                       "'" + addBookings.B_BookingDate + "'," +
                                       "'" + addBookings.B_PreferredDate + "'," +
                                       "'A'," +
                                       "'" + addBookings.B_BookingStatus + "')";



                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(Query))
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