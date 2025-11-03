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

            string Query = @"
        SELECT 
            b.BookingID,
            b.CustomerID,
            b.VehicleID,
            b.BookingDate,
            b.PreferredDate,
            b.BookingStatus,
            b.Status,
            b.timeslotID,
            c.FullName AS CustomerName,
            c.Phone AS CustomerPhone,
            t.start_time,
            t.end_time,
            STRING_AGG(s.ServiceName, ', ') AS ServiceNames
        FROM 
            Bookings b
        LEFT JOIN 
            Customers c ON b.CustomerID = c.CustomerID
        LEFT JOIN 
            Timeslot t ON b.timeslotID = t.timeslotID
        LEFT JOIN 
            BookingServices bs ON b.BookingID = bs.BookingID
        LEFT JOIN 
            Services s ON bs.ServiceID = s.ServiceID
        GROUP BY 
            b.BookingID, b.CustomerID, b.VehicleID, b.BookingDate, b.PreferredDate, 
            b.BookingStatus, b.Status, b.timeslotID, c.FullName, c.Phone, t.start_time, t.end_time";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {
                        GetBookingsModal booking = new GetBookingsModal
                        {
                            B_BookingID = reader["BookingID"].ToString(),
                            B_CustomerID = reader["CustomerID"].ToString(),
                            B_VehicleID = reader["VehicleID"].ToString(),
                            B_BookingDate = reader["BookingDate"].ToString(),
                            B_PreferredDate = reader["PreferredDate"] == DBNull.Value ? null : reader["PreferredDate"].ToString(),
                            B_BookingStatus = reader["BookingStatus"].ToString(),
                            B_Status = reader["Status"].ToString(),
                            B_timeslotID = reader["timeslotID"].ToString(),
                            B_CustomerName = reader["CustomerName"].ToString(),
                            B_CustomerPhone = reader["CustomerPhone"].ToString(),
                            B_StartTime = reader["start_time"].ToString(),
                            B_EndTime = reader["end_time"].ToString(),
                            B_ServiceName = reader["ServiceNames"] == DBNull.Value ? "" : reader["ServiceNames"].ToString()
                        };

                        BookingsList.Add(booking);
                    }
                }
            }

            res.StatusCode = 200;
            res.ResultSet = BookingsList;
            return res;
        }





        /*
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




        */

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






        //public Response GetBookingsByBookingID(string BookingID)
        //{
        //    Response res = new Response();
        //    List<GetBookingsModal> BookingsList = new List<GetBookingsModal>();

        //    string Query = "SELECT " +
        //                        "BookingID, " +
        //                        "CustomerID," +
        //                        "VehicleID, " +
        //                        "BookingDate," +
        //                        "PreferredDate, " +
        //                        "BookingStatus " +
        //                    "FROM " +
        //                             "Bookings " +
        //                    "WHERE " +
        //                             "BookingID = '" + BookingID + "'";
        //    using (var DBconnect = new DBconnect())
        //    {
        //        using (SqlDataReader reader = DBconnect.ReadTable(Query))
        //        {
        //            while (reader.Read())
        //            {

        //                GetBookingsModal Bookings = new GetBookingsModal
        //                {
        //                    B_BookingID = reader["BookingID"].ToString(),
        //                    B_CustomerID = reader["CustomerID"].ToString(),
        //                    B_VehicleID = reader["VehicleID"].ToString(),
        //                    B_BookingDate = reader["BookingDate"].ToString(),
        //                    B_PreferredDate = reader["PreferredDate"].ToString(),
        //                    B_BookingStatus = reader["BookingStatus"].ToString()
        //                };
        //                BookingsList.Add(Bookings);
        //            }
        //        }
        //    }
        //    res.StatusCode = 200;
        //    res.ResultSet = BookingsList;
        //    return res;
        //}
        public Response GetBookingsByBookingID(string BookingID)
        {
            Response res = new Response();
            List<GetBookingsModal> BookingsList = new List<GetBookingsModal>();

            string Query = @"
                                                        SELECT 
                                                            b.BookingID,
                                                            b.CustomerID,
                                                            b.VehicleID,
                                                            b.BookingDate,
                                                            b.PreferredDate,
                                                            b.BookingStatus,
                                                            b.Status,
                                                            b.timeslotID,
                                                            c.FullName AS CustomerName,
                                                            c.Phone AS CustomerPhone,
                                                            t.start_time,
                                                            t.end_time,
                                                            STRING_AGG(s.ServiceName, ', ') AS ServiceNames
                                                        FROM 
                                                            Bookings b
                                                        LEFT JOIN 
                                                            Customers c ON b.CustomerID = c.CustomerID
                                                        LEFT JOIN 
                                                            Timeslot t ON b.timeslotID = t.timeslotID
                                                        LEFT JOIN 
                                                            BookingServices bs ON b.BookingID = bs.BookingID
                                                        LEFT JOIN 
                                                            Services s ON bs.ServiceID = s.ServiceID
                                                        WHERE 
                                                            b.BookingID = @BookingID
                                                        GROUP BY 
                                                            b.BookingID, b.CustomerID, b.VehicleID, b.BookingDate, b.PreferredDate, 
                                                            b.BookingStatus, b.Status, b.timeslotID, c.FullName, c.Phone, t.start_time, t.end_time";

            using (var DBconnect = new DBconnect())
            {
                using (SqlConnection conn = DBconnect.GetOpenConnection())
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingID", BookingID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GetBookingsModal booking = new GetBookingsModal
                            {
                                B_BookingID = reader["BookingID"].ToString(),
                                B_CustomerID = reader["CustomerID"].ToString(),
                                B_VehicleID = reader["VehicleID"].ToString(),
                                B_BookingDate = reader["BookingDate"].ToString(),
                                B_PreferredDate = reader["PreferredDate"] == DBNull.Value ? null : reader["PreferredDate"].ToString(),
                                B_BookingStatus = reader["BookingStatus"].ToString(),
                                B_Status = reader["Status"].ToString(),
                                B_timeslotID = reader["timeslotID"].ToString(),
                                B_CustomerName = reader["CustomerName"].ToString(),
                                B_CustomerPhone = reader["CustomerPhone"].ToString(),
                                B_StartTime = reader["start_time"].ToString(),
                                B_EndTime = reader["end_time"].ToString(),
                                B_ServiceName = reader["ServiceNames"] == DBNull.Value ? "" : reader["ServiceNames"].ToString()
                            };

                            BookingsList.Add(booking);
                        }
                    }
                }
            }

            if (BookingsList.Count > 0)
            {
                res.StatusCode = 200;
                res.ResultSet = BookingsList;
                //res.Message = "Booking details fetched successfully.";
            }
            else
            {
                res.StatusCode = 404;
                res.ResultSet = null;
                //res.Message = "No booking found for this BookingID.";
            }

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











        //public Response PutBookingTimeDetails(GetBookingsModal addBookings)
        //{
        //    Response res = new Response();
        //    DBconnect DBconnect = new DBconnect();
        //    try
        //    {
        //        // Update booking starting and ending times
        //        string updateQuery = @"UPDATE Bookings
        //                       SET StartingTime = '" + addBookings.B_StartingTime + @"',
        //                           EndingTime = '" + addBookings.B_EndingTime + @"'
        //                       WHERE BookingID = '" + addBookings.B_BookingID + @"'";

        //        using (var dbConnect = new DBconnect())
        //        {
        //            if (dbConnect.AddEditDel(updateQuery))
        //            {
        //                // Fetch customer's phone number for this booking
        //                string getCustomerQuery = @"SELECT c.Phone
        //                                    FROM Customers c
        //                                    INNER JOIN Bookings b ON c.CustomerID = b.CustomerID
        //                                    WHERE b.BookingID = '" + addBookings.B_BookingID + @"'";

        //                string customerPhone = dbConnect.ExecuteScalar(getCustomerQuery)?.ToString();

        //                // Send SMS if phone is found
        //                if (!string.IsNullOrEmpty(customerPhone))
        //                {
        //                    // Prepare the message for time update
        //                    string message = "Dear Customer, your AudoDeck booking time has been updated.\n" +
        //                                     "New Starting Time: " + addBookings.B_StartingTime +
        //                                     "\nNew Ending Time: " + addBookings.B_EndingTime;
        //                    SendTimeUpdateSMS(customerPhone, message);  // Send time update SMS
        //                }

        //                res.StatusCode = 200;
        //                res.Result = "Success!! Booking time updated and SMS sent.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        res.StatusCode = 500;
        //        res.Result = "Failed!!";
        //    }
        //    return res;
        //}

        // SMS sending function using your service
        private void SendTimeUpdateSMS(string contact, string message)
        {
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    // URL encode the message to handle special characters
                    string encodedMessage = System.Web.HttpUtility.UrlEncode(message);

                    // DTS API URL to send the SMS
                    string smsApiUrl = $"https://esystems.cdl.lk/Backend/SMSGateway/api/SMS/DTSSendMessage?mobileNo={contact}&message={encodedMessage}";

                    var response = client.GetAsync(smsApiUrl).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        LogHandler.WriteToLog($"SMS sending failed to {contact}", "SendTimeUpdateSMS");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, "SendTimeUpdateSMS");
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











        //public Response AddBookingsDetails(GetBookingsModal addBookings)
        //{
        //    Response res = new Response();

        //    try
        //    {
        //        string Query = @"INSERT INTO Bookings 
        //                                (CustomerID, 
        //                                 VehicleID, 
        //                                 BookingDate,
        //                                 BookingStatus, 
        //                                 Status, 
        //                                 timeslotID, 
        //                                 ServiceID)
        //                          VALUES
        //                                ('" + addBookings.B_CustomerID + @"',
        //                                 '" + addBookings.B_VehicleID + @"',
        //                                 '" + addBookings.B_BookingDate + @"',
        //                                 '" + addBookings.B_BookingStatus + @"',
        //                                 'A',
        //                                 '" + addBookings.B_timeslotID + @"',
        //                                 '" + addBookings.B_ServiceID + @"')";  // ✅ removed the extra quote before parenthesis

        //        using (var dbConnect = new DBconnect())
        //        {
        //            if (dbConnect.AddEditDel(Query))
        //            {
        //                res.StatusCode = 200;
        //                res.Result = "Success!!";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        res.StatusCode = 500;
        //        res.Result = "Failed!! " + ex.Message;  // ✅ include error details for debugging
        //    }

        //    return res;
        //}
        //public Response AddBookingsDetails(GetBookingsModal addBookings)
        //{
        //    Response res = new Response();

        //    try
        //    {
        //        DBconnect dbConnect = new DBconnect();

        //        // 1️⃣ Insert booking (without ServiceID) and return new BookingID
        //        string insertBookingQuery = @"INSERT INTO Bookings 
        //                        (CustomerID, 
        //                         VehicleID, 
        //                         BookingDate,
        //                         BookingStatus, 
        //                         Status, 
        //                         timeslotID)
        //                  OUTPUT INSERTED.BookingID
        //                  VALUES
        //                        ('" + addBookings.B_CustomerID + @"',
        //                         '" + addBookings.B_VehicleID + @"',
        //                         '" + addBookings.B_BookingDate + @"',
        //                         '" + addBookings.B_BookingStatus + @"',
        //                         'A',
        //                         '" + addBookings.B_timeslotID + @"')";

        //        // Get the newly created BookingID using your ExecuteScalar method
        //        object result = dbConnect.ExecuteScalar(insertBookingQuery);
        //        int newBookingID = (result != null) ? Convert.ToInt32(result) : 0;

        //        // 2️⃣ Insert multiple services into BookingServices table
        //        if (newBookingID > 0 && addBookings.B_ServiceIDs != null && addBookings.B_ServiceIDs.Count > 0)
        //        {
        //            foreach (var serviceID in addBookings.B_ServiceIDs)
        //            {
        //                string insertServiceQuery = @"INSERT INTO BookingServices 
        //                                                           (BookingID,
        //                                                            ServiceID)
        //                                      VALUES  
        //                                       ('" + newBookingID + "', " +
        //                                       " '" + serviceID + "')";
        //                dbConnect.AddEditDel(insertServiceQuery);
        //            }
        //        }

        //        res.StatusCode = 200;
        //        res.Result = "Success!! Booking and multiple services added.";
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
        //        res.StatusCode = 500;
        //        res.Result = "Failed!! " + ex.Message;
        //    }

        //    return res;
        //}
        public Response AddBookingsDetails(GetBookingsModal addBookings)
        {
            Response res = new Response();

            try
            {
                DBconnect dbConnect = new DBconnect();

                // 1️⃣ Insert booking and return new BookingID
                string insertBookingQuery = @"INSERT INTO Bookings 
                        (CustomerID, 
                         VehicleID, 
                         BookingDate,
                         BookingStatus, 
                         Status, 
                         timeslotID)
                  OUTPUT INSERTED.BookingID
                  VALUES
                        ('" + addBookings.B_CustomerID + @"',
                         '" + addBookings.B_VehicleID + @"',
                         '" + addBookings.B_BookingDate + @"',
                         '" + addBookings.B_BookingStatus + @"',
                         'A',
                         '" + addBookings.B_timeslotID + @"')";

                object result = dbConnect.ExecuteScalar(insertBookingQuery);
                int newBookingID = (result != null) ? Convert.ToInt32(result) : 0;

                // 2️⃣ Insert multiple services into BookingServices
                if (newBookingID > 0 && addBookings.B_ServiceIDs != null && addBookings.B_ServiceIDs.Count > 0)
                {
                    foreach (var serviceID in addBookings.B_ServiceIDs)
                    {
                        string insertServiceQuery = @"INSERT INTO BookingServices 
                                               (BookingID, ServiceID)
                                               VALUES ('" + newBookingID + "', '" + serviceID + "')";
                        dbConnect.AddEditDel(insertServiceQuery);
                    }
                }

                // 3️⃣ Automatically create a new JobCard for this booking
                if (newBookingID > 0)
                {
                    string insertJobCardQuery = @"INSERT INTO JobCards
                        (BookingID, CreatedDate, Technician, JobCardStatus, Status)
                  VALUES
                        ('" + newBookingID + @"',
                         GETDATE(),
                         'Unassigned',   -- or assign based on logic
                         'Pending',
                         'A')";

                    dbConnect.AddEditDel(insertJobCardQuery);
                }

                res.StatusCode = 200;
                res.Result = "Success!! Booking and JobCard created.";
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed!! " + ex.Message;
            }

            return res;
        }










        //public Response GetBookingsByCustomerID(string CustomerID)
        //{
        //    Response res = new Response();
        //    List<GetBookingsModal> BookingsList = new List<GetBookingsModal>();

        //    // Modify the query to include a JOIN with the Customers table to fetch FullName
        //    string Query = "SELECT " +
        //                    "b.BookingID, " +
        //                    "b.CustomerID, " +
        //                    "b.VehicleID, " +
        //                    "b.BookingDate, " +
        //                    "b.PreferredDate, " +
        //                    "b.BookingStatus, " +
        //                    "c.FullName AS CustomerName " +  // Select FullName from Customers table
        //                    "FROM " +
        //                    "Bookings b " +
        //                    "JOIN Customers c ON b.CustomerID = c.CustomerID " +  // Join condition
        //                    "WHERE " +
        //                    "b.CustomerID = '" + CustomerID + "'";

        //    using (var DBconnect = new DBconnect())
        //    {
        //        using (SqlDataReader reader = DBconnect.ReadTable(Query))
        //        {
        //            while (reader.Read())
        //            {
        //                // Map the result to GetBookingsModal
        //                GetBookingsModal Bookings = new GetBookingsModal
        //                {
        //                    B_BookingID = reader["BookingID"].ToString(),
        //                    B_CustomerID = reader["CustomerID"].ToString(),
        //                    B_VehicleID = reader["VehicleID"].ToString(),
        //                    B_BookingDate = reader["BookingDate"].ToString(),
        //                    B_PreferredDate = reader["PreferredDate"].ToString(),
        //                    B_BookingStatus = reader["BookingStatus"].ToString(),
        //                    B_CustomerName = reader["CustomerName"].ToString()  // New field for customer name
        //                };
        //                BookingsList.Add(Bookings);
        //            }
        //        }
        //    }

        //    res.StatusCode = 200;
        //    res.ResultSet = BookingsList;
        //    return res;
        //}
        public Response GetBookingsByCustomerID(string CustomerID)
        {
            Response res = new Response();
            List<GetBookingsModal> BookingsList = new List<GetBookingsModal>();

            string Query = @"
SELECT 
    b.BookingID,
    b.CustomerID,
    b.VehicleID,
    b.BookingDate,
    b.PreferredDate,
    b.BookingStatus,
    b.Status,
    b.timeslotID,
    c.FullName AS CustomerName,
    c.Phone AS CustomerPhone,
    t.start_time,
    t.end_time,
    STRING_AGG(s.ServiceName, ', ') AS ServiceNames
FROM 
    Bookings b
LEFT JOIN 
    Customers c ON b.CustomerID = c.CustomerID
LEFT JOIN 
    Timeslot t ON b.timeslotID = t.timeslotID
LEFT JOIN 
    BookingServices bs ON b.BookingID = bs.BookingID
LEFT JOIN 
    Services s ON bs.ServiceID = s.ServiceID
WHERE 
    b.CustomerID = @CustomerID
GROUP BY 
    b.BookingID, b.CustomerID, b.VehicleID, b.BookingDate, b.PreferredDate, 
    b.BookingStatus, b.Status, b.timeslotID, c.FullName, c.Phone, t.start_time, t.end_time";

            using (var DBconnect = new DBconnect())
            {
                using (SqlConnection conn = DBconnect.GetOpenConnection())
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GetBookingsModal booking = new GetBookingsModal
                            {
                                B_BookingID = reader["BookingID"].ToString(),
                                B_CustomerID = reader["CustomerID"].ToString(),
                                B_VehicleID = reader["VehicleID"].ToString(),
                                B_BookingDate = reader["BookingDate"].ToString(),
                                B_PreferredDate = reader["PreferredDate"] == DBNull.Value ? null : reader["PreferredDate"].ToString(),
                                B_BookingStatus = reader["BookingStatus"].ToString(),
                                B_Status = reader["Status"].ToString(),
                                B_timeslotID = reader["timeslotID"].ToString(),
                                B_CustomerName = reader["CustomerName"].ToString(),
                                B_CustomerPhone = reader["CustomerPhone"].ToString(),
                                B_StartTime = reader["start_time"].ToString(),
                                B_EndTime = reader["end_time"].ToString(),
                                B_ServiceName = reader["ServiceNames"] == DBNull.Value ? "" : reader["ServiceNames"].ToString()
                            };

                            BookingsList.Add(booking);
                        }
                    }
                }
            }

            if (BookingsList.Count > 0)
            {
                res.StatusCode = 200;
                res.ResultSet = BookingsList;
                //res.Message = "Bookings fetched successfully for the given CustomerID.";
            }
            else
            {
                res.StatusCode = 404;
                res.ResultSet = null;
                //res.Message = "No bookings found for this CustomerID.";
            }

            return res;
        }



    }
}