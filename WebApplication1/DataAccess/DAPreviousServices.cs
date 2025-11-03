using biZTrack.Static;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DAPreviousServices : IPreviousServices
    {
        public Response GetAllPreviousServices()
        {
            Response res = new Response();
            List<GetPreviousServicesModal> servicesList = new List<GetPreviousServicesModal>();

            string Query = @"
                SELECT 
                    ps.ServiceID,
                    ps.CustomerID,
                    ps.ServiceName,
                    ps.PlaceName,
                    ps.Price,
                    ps.ServiceDate,
                    ps.Notes,
                    c.FullName AS CustomerName,
                    c.Phone AS CustomerPhone
                FROM PreviousServices ps
                LEFT JOIN Customers c ON ps.CustomerID = c.CustomerID";

            using (var DBconnect = new DBconnect())
            using (SqlDataReader reader = DBconnect.ReadTable(Query))
            {
                while (reader.Read())
                {
                    var service = new GetPreviousServicesModal
                    {
                        PS_ServiceID = reader["ServiceID"].ToString(),
                        PS_CustomerID = reader["CustomerID"].ToString(),
                        PS_ServiceName = reader["ServiceName"].ToString(),
                        PS_PlaceName = reader["PlaceName"].ToString(),
                        PS_Price = Convert.ToDecimal(reader["Price"]),
                        PS_ServiceDate = reader["ServiceDate"].ToString(),
                        PS_Notes = reader["Notes"].ToString(),
                       
                    };
                    servicesList.Add(service);
                }
            }

            res.StatusCode = 200;
            res.ResultSet = servicesList;
            return res;
        }

        public Response GetPreviousServicesByCustomerID(string CustomerID)
        {
            Response res = new Response();
            List<GetPreviousServicesModal> servicesList = new List<GetPreviousServicesModal>();

            string Query = @"
                SELECT 
                    ps.ServiceID,
                    ps.CustomerID,
                    ps.ServiceName,
                    ps.PlaceName,
                    ps.Price,
                    ps.ServiceDate,
                    ps.Notes,
                    c.FullName AS CustomerName,
                    c.Phone AS CustomerPhone
                FROM PreviousServices ps
                LEFT JOIN Customers c ON ps.CustomerID = c.CustomerID
                WHERE ps.CustomerID = @CustomerID";

            using (var DBconnect = new DBconnect())
            using (SqlConnection conn = DBconnect.GetOpenConnection())
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var service = new GetPreviousServicesModal
                        {
                            PS_ServiceID = reader["ServiceID"].ToString(),
                            PS_CustomerID = reader["CustomerID"].ToString(),
                            PS_ServiceName = reader["ServiceName"].ToString(),
                            PS_PlaceName = reader["PlaceName"].ToString(),
                            PS_Price = Convert.ToDecimal(reader["Price"]),
                            PS_ServiceDate = reader["ServiceDate"].ToString(),
                            PS_Notes = reader["Notes"].ToString(),
                           
                        };
                        servicesList.Add(service);
                    }
                }
            }

            res.StatusCode = 200;
            res.ResultSet = servicesList;
            return res;
        }

        public Response AddPreviousService(GetPreviousServicesModal previousService)
        {
            Response res = new Response();
            try
            {
                string Query = @"INSERT INTO PreviousServices
                                (CustomerID, ServiceName, PlaceName, Price, ServiceDate, Notes)
                                 VALUES
                                (@CustomerID, @ServiceName, @PlaceName, @Price, @ServiceDate, @Notes)";

                using (var DBconnect = new DBconnect())
                using (SqlConnection conn = DBconnect.GetOpenConnection())
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", previousService.PS_CustomerID);
                    cmd.Parameters.AddWithValue("@ServiceName", previousService.PS_ServiceName);
                    cmd.Parameters.AddWithValue("@PlaceName", previousService.PS_PlaceName);
                    cmd.Parameters.AddWithValue("@Price", previousService.PS_Price);
                    cmd.Parameters.AddWithValue("@ServiceDate", previousService.PS_ServiceDate);
                    cmd.Parameters.AddWithValue("@Notes", (object)previousService.PS_Notes ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                res.StatusCode = 200;
                res.Result = "Success!! Previous service record added.";
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, "AddPreviousService");
                res.StatusCode = 500;
                res.Result = "Failed!! " + ex.Message;
            }
            return res;
        }

        public Response DeletePreviousService(string ServiceID)
        {
            Response res = new Response();
            try
            {
                string Query = @"DELETE FROM PreviousServices WHERE ServiceID = @ServiceID";

                using (var DBconnect = new DBconnect())
                using (SqlConnection conn = DBconnect.GetOpenConnection())
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ServiceID", ServiceID);
                    cmd.ExecuteNonQuery();
                }

                res.StatusCode = 200;
                res.Result = "Success!! Previous service record deleted.";
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, "DeletePreviousService");
                res.StatusCode = 500;
                res.Result = "Failed!! " + ex.Message;
            }
            return res;
        }
    }
}
