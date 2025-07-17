using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Xml.Linq;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DAService : IService
    {
        public object serviceList;

        public object ServiceList { get; private set; }

        public Response GetAllServices()

        {
            Response res = new Response();
            List<GetServiceModal> ServiceList = new List<GetServiceModal>();

            string Query = "SELECT " +
                                "id, " +
                                "cost," +
                                "description, " +
                                "name, " +
                                "CategoryId, " +
                                "slots, " +
                                "time_period, " +

                            "FROM " +
                                "services ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetServiceModal service = new GetServiceModal
                        {
                            S_ID = reader["id"].ToString(),
                            S_Cost = reader["cost"].ToString(),
                            S_Description = reader["description"].ToString(),
                            S_Name = reader["name"].ToString(),
                            S_CategoryId = reader["CategoryId"].ToString(),
                            S_Slots = reader["slots"].ToString(),
                            S_TimePeriod = reader["time_period"].ToString()
                        };


                        ServiceList.Add(service);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = serviceList;
            return res;


        }

        public Response GetServiceById(string id)
        {


            Response res = new Response();
            List<GetServiceModal> VehicleList = new List<GetServiceModal>();

            string Query = "SELECT " +
                                    "id, " +
                                    "cost," +
                                    "description, " +
                                    "name, " +
                                    "CategoryId, " +
                                    "slots, " +
                                    "time_period, " +

                               "FROM " +
                                "service " +
                            "WHERE " +
                                "id = '" + id + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetServiceModal service = new GetServiceModal
                        {
                            S_ID = reader["id"].ToString(),
                            S_Cost = reader["cost"].ToString(),
                            S_Description = reader["description"].ToString(),
                            S_Name = reader["name"].ToString(),
                            S_CategoryId = reader["CategoryId"].ToString(),
                            S_Slots = reader["slots"].ToString(),
                            S_TimePeriod = reader["time_period"].ToString()
                        };
                        ServiceList.Add(service);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = VehicleList;
            return res;
        }
    }
} 