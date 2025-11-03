using biZTrack.Static;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;
namespace WebApplication1.DataAccess
{
    public class DAStatus : IStatus
    {
        public Response GetAllStatus()
        {
            Response res = new Response();
            List<GetStatusModal> StatusList = new List<GetStatusModal>();

            string Query = "SELECT " +
                                            "c.CustomerID, " +
                                            "c.Phone, " +
                                            "b.BookingID, " +
                                            "b.BookingStatus " +
                                       "FROM " +
                                            "[Car_Service].[dbo].[Bookings] b " +
                                       "INNER JOIN " +
                                            "[Car_Service].[dbo].[Customers] c " +
                                       "ON b.CustomerID = c.CustomerID";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetStatusModal Status = new GetStatusModal();


                        Status.S_CustomerID = reader["CustomerID"].ToString();
                        Status.s_Phone = reader["Phone"].ToString();
                        Status.S_BookingID = reader["BookingID"].ToString();
                        Status.S_BookingStatus = reader["BookingStatus"].ToString();

                        StatusList.Add(Status);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = StatusList;
            return res;
        }
    }
}