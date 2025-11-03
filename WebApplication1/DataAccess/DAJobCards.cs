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
    public class DAJobCards: IJobCards
    {
        //public Response AddJobCardsDetails(GetJobCardsModal addJobCards)
        //{

        //    Response res = new Response();
        //    DBconnect DBconnect = new DBconnect();
        //    try
        //    {
        //        string Query = "INSERT INTO JobCards " +
        //                                  "(BookingID," +
        //                                   "CreatedDate," +
        //                                   "Technician," +
        //                                   "Status," +
        //                                   "JobCardStatus) " +
        //                       "VALUES('" + addJobCards.J_BookingID + "'," +
        //                               "'" + addJobCards.J_CreatedDate + "'," +
        //                               "'" + addJobCards.J_Technician + "'," +
        //                               "'A'," +
        //                               "'" + addJobCards.J_JobCardStatus + "')";



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
        //        res.Result = "Failed!!";
        //    }
        //    return res;
        //}
        public Response AddJobCardsDetails(GetJobCardsModal addJobCards)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = @"INSERT INTO JobCards 
                        (   BookingID,
                            CreatedDate,
                            Technician,
                            Status, 
                            JobCardStatus, 
                            Quantity,
                            Price)
                        VALUES (
                            '" + addJobCards.J_BookingID + @"',
                            '" + addJobCards.J_CreatedDate + @"',
                            '" + addJobCards.J_Technician + @"',
                            'A',
                            '" + addJobCards.J_JobCardStatus + @"',
                            '" + addJobCards.Quantity + @"',
                            '" + addJobCards.Price + @"'
                        )";

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

        public Response DeleteJobCardsDetails(GetJobCardsModal addJobCards)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE JobCards
                                        SET Status = 'I'
                                        WHERE JobCardID = '" + addJobCards.J_JobCardID + @"'";


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

        //public Response GetAllJobCards()

        //{
        //    Response res = new Response();
        //    List<GetJobCardsModal> JobCardsList = new List<GetJobCardsModal>();

        //    string Query = "SELECT " +
        //                        "JobCardID, " +
        //                        "BookingID," +
        //                        "CreatedDate, " +
        //                        "Technician, " +
        //                        "JobCardStatus " +
        //                    "FROM " +
        //                        "JobCards ";

        //    using (var DBconnect = new DBconnect())
        //    {
        //        using (SqlDataReader reader = DBconnect.ReadTable(Query))
        //        {
        //            while (reader.Read())
        //            {

        //                GetJobCardsModal JobCards = new GetJobCardsModal
        //                {
        //                    J_JobCardID = reader["JobCardID"].ToString(),
        //                    J_BookingID = reader["BookingID"].ToString(),
        //                    J_CreatedDate = reader["CreatedDate"].ToString(),
        //                    J_Technician = reader["Technician"].ToString(),
        //                    J_JobCardStatus = reader["JobCardStatus"].ToString()
        //                };


        //                JobCardsList.Add(JobCards);
        //            }
        //        }
        //    }
        //    res.StatusCode = 200;
        //    res.ResultSet = JobCardsList;
        //    return res;


        //}
        public Response GetAllJobCards()
        {
            Response res = new Response();
            List<GetJobCardsModal> JobCardsList = new List<GetJobCardsModal>();

            // Updated query to join with the Vehicles and Technicians tables
            string Query = @"
    SELECT 
        j.JobCardID, 
        j.BookingID,
        j.CreatedDate, 
        t.FullName AS TechnicianName, 
        j.JobCardStatus,
        j.Status AS JobCardStatus,
        j.Quantity,
        j.Price,
        b.CustomerID,
        b.BookingDate,
        b.PreferredDate,
        v.PlateNumber,
        v.Make,
        v.Model,
        v.Year,
        v.VIN
    FROM JobCards j
    LEFT JOIN Technicians t ON j.TechnicianID = t.TechnicianID
    LEFT JOIN Bookings b ON j.BookingID = b.BookingID
    LEFT JOIN Vehicles v ON b.VehicleID = v.VehicleID
    WHERE j.Status = 'A'";  // Assuming you're only fetching active job cards

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {
                        GetJobCardsModal JobCards = new GetJobCardsModal
                        {
                            J_JobCardID = reader["JobCardID"].ToString(),
                            J_BookingID = reader["BookingID"].ToString(),
                            J_CreatedDate = reader["CreatedDate"].ToString(),
                            // Technician name
                            J_Technician = reader["TechnicianName"].ToString(),
                            J_JobCardStatus = reader["JobCardStatus"].ToString(),
                            J_Status = reader["JobCardStatus"].ToString(),
                            Quantity = reader["Quantity"].ToString(),
                            Price = reader["Price"].ToString(),
                            // Vehicle details
                            VehiclePlateNumber = reader["PlateNumber"].ToString(),
                            VehicleMake = reader["Make"].ToString(),
                            VehicleModel = reader["Model"].ToString(),
                            VehicleYear = reader["Year"].ToString(),
                            VehicleVIN = reader["VIN"].ToString(),
                            ServiceNames = "" // This will be populated in the next query
                        };

                        // Get all related service names for this booking
                        string serviceQuery = @"
                SELECT s.ServiceName
                FROM BookingServices bs
                INNER JOIN Services s ON bs.ServiceID = s.ServiceID
                WHERE bs.BookingID = '" + JobCards.J_BookingID + "'";

                        using (var dbConnect2 = new DBconnect())
                        {
                            using (SqlDataReader serviceReader = dbConnect2.ReadTable(serviceQuery))
                            {
                                List<string> services = new List<string>();
                                while (serviceReader.Read())
                                {
                                    services.Add(serviceReader["ServiceName"].ToString());
                                }

                                JobCards.ServiceNames = string.Join(", ", services);
                            }
                        }

                        JobCardsList.Add(JobCards);
                    }
                }
            }

            res.StatusCode = 200;
            res.ResultSet = JobCardsList;
            return res;
        }

        public Response GetJobCardsByJobCardID(string JobCardID)
        {
            Response res = new Response();
            List<GetJobCardsModal> JobCardsList = new List<GetJobCardsModal>();

            string Query = "SELECT " +
                                "JobCardID, " +
                                "BookingID," +
                                "CreatedDate, " +
                                "Technician, " +
                                "JobCardStatus " +
                            "FROM " +
                                     "JobCards " +
                            "WHERE " +
                                     "JobCardID = '" + JobCardID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetJobCardsModal JobCards = new GetJobCardsModal
                        {
                            J_JobCardID = reader["JobCardID"].ToString(),
                            J_BookingID = reader["BookingID"].ToString(),
                            J_CreatedDate = reader["CreatedDate"].ToString(),
                            J_Technician = reader["Technician"].ToString(),
                            J_JobCardStatus = reader["JobCardStatus"].ToString()
                        };
                        JobCardsList.Add(JobCards);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = JobCardsList;
            return res;
        }

        public Response PutJobCardsDetails(GetJobCardsModal addJobCards)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE JobCards
                                        SET TechnicianID = '" + addJobCards.J_TechnicianID + @"',
                                            JobCardStatus = '" + addJobCards.J_JobCardStatus + @"'
                                        WHERE JobCardID = '" + addJobCards.J_JobCardID + @"'";


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
        public Response UpdateBookingServices(GetJobCardsModal addJobCards)
        {
            Response res = new Response();

            try
            {
                using (var dbConnect = new DBconnect())
                {
                    if (string.IsNullOrEmpty(addJobCards.J_BookingID))
                    {
                        res.StatusCode = 400;
                        res.Result = "BookingID is required!";
                        return res;
                    }

                    // Step 1️⃣: Check if booking exists
                    string checkBooking = "SELECT COUNT(*) FROM Bookings WHERE BookingID = '" + addJobCards.J_BookingID + "'";
                    int bookingExists = Convert.ToInt32(dbConnect.ExecuteScalar(checkBooking));

                    if (bookingExists == 0)
                    {
                        res.StatusCode = 404;
                        res.Result = "Booking not found!";
                        return res;
                    }

                    // Step 2️⃣: Add new services if not already added
                    if (addJobCards.NewServiceIDs != null && addJobCards.NewServiceIDs.Count > 0)
                    {
                        foreach (var serviceId in addJobCards.NewServiceIDs)
                        {
                            // Check if this service already exists for the booking
                            string checkQuery = @"SELECT COUNT(*) FROM BookingServices
                                          WHERE BookingID = '" + addJobCards.J_BookingID + @"'
                                          AND ServiceID = '" + serviceId + "'";

                            int exists = Convert.ToInt32(dbConnect.ExecuteScalar(checkQuery));

                            if (exists == 0)
                            {
                                // Insert new service for this booking
                                string insertQuery = @"INSERT INTO BookingServices (BookingID, ServiceID)
                                               VALUES ('" + addJobCards.J_BookingID + @"', '" + serviceId + @"')";
                                dbConnect.AddEditDel(insertQuery);
                            }
                        }

                        res.StatusCode = 200;
                        res.Result = "Services added successfully for BookingID " + addJobCards.J_BookingID;
                    }
                    else
                    {
                        res.StatusCode = 400;
                        res.Result = "No services provided!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed! " + ex.Message;
            }

            return res;
        }
        public Response AddBookingParts(GetJobCardsModal addJobCards)
        {
            Response res = new Response();

            try
            {
                using (var dbConnect = new DBconnect())
                {
                    if (string.IsNullOrEmpty(addJobCards.J_BookingID))
                    {
                        res.StatusCode = 400;
                        res.Result = "BookingID is required!";
                        return res;
                    }

                    if (addJobCards.PartsList == null || addJobCards.PartsList.Count == 0)
                    {
                        res.StatusCode = 400;
                        res.Result = "No parts provided!";
                        return res;
                    }

                    foreach (var part in addJobCards.PartsList)
                    {
                        string insertQuery = @"INSERT INTO BookingParts (BookingID, PartID, Quantity)
                                       VALUES ('" + addJobCards.J_BookingID + "', '" + part.PartID + "', '" + part.Quantity + "')";
                        dbConnect.AddEditDel(insertQuery);
                    }

                    res.StatusCode = 200;
                    res.Result = "Parts added successfully for BookingID " + addJobCards.J_BookingID;
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed! " + ex.Message;
            }

            return res;
        }
        public Response GetBookingPartsByBookingID(string BookingID)
        {
            Response res = new Response();
            List<object> partsList = new List<object>();

            try
            {
                string query = @"SELECT 
                            bp.BookingPartID,
                            bp.BookingID,
                            p.PartID,
                            p.PartName,
                            bp.Quantity,
                            p.UnitPrice,
                            (bp.Quantity * p.UnitPrice) AS TotalPrice
                         FROM BookingParts bp
                         INNER JOIN PartsInventory p ON bp.PartID = p.PartID
                         WHERE bp.BookingID = '" + BookingID + "'";

                using (var dbConnect = new DBconnect())
                {
                    using (SqlDataReader reader = dbConnect.ReadTable(query))
                    {
                        while (reader.Read())
                        {
                            partsList.Add(new
                            {
                                BookingPartID = reader["BookingPartID"].ToString(),
                                BookingID = reader["BookingID"].ToString(),
                                PartID = reader["PartID"].ToString(),
                                PartName = reader["PartName"].ToString(),
                                Quantity = reader["Quantity"].ToString(),
                                UnitPrice = reader["UnitPrice"].ToString(),
                                TotalPrice = reader["TotalPrice"].ToString()
                            });
                        }
                    }
                }

                res.StatusCode = 200;
                res.Result = "Success!";
                res.ResultSet = partsList;
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                res.Result = "Failed! " + ex.Message;
            }

            return res;
        }



    }
}