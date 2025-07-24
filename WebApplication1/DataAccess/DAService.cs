using biZTrack.Static;
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


        public Response GetAllServices()

        {
            Response res = new Response();
            List<GetServiceModal> ServiceList = new List<GetServiceModal>();

            string Query = "SELECT " +
                                "id, " +
                                "cost," +
                                "description, " +
                                "name, " +
                                "category_id, " +
                                "slots, " +
                                "time_period " +

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
                            S_CategoryId = reader["category_id"].ToString(),
                            S_Slots = reader["slots"].ToString(),
                            S_TimePeriod = reader["time_period"].ToString()
                        };


                        ServiceList.Add(service);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = ServiceList;
            return res;


        }

        public Response GetServiceById(string id)
        {


            Response res = new Response();
            List<GetServiceModal> ServiceList = new List<GetServiceModal>();

            string Query = "SELECT " +
                                    "id, " +
                                    "cost," +
                                    "description, " +
                                    "name, " +
                                    "category_id, " +
                                    "slots, " +
                                    "time_period " +
                            "FROM " +
                                     "services " +
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
                            S_CategoryId = reader["category_id"].ToString(),
                            S_Slots = reader["slots"].ToString(),
                            S_TimePeriod = reader["time_period"].ToString()
                        };
                        ServiceList.Add(service);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = ServiceList;
            return res;
        }
        public Response GetServiceBycategoryId(string id)
        {
            Response res = new Response();
            List<GetServiceModal> ServiceList = new List<GetServiceModal>();

            string Query = "SELECT " +
                                    "id, " +
                                    "cost," +
                                    "description, " +
                                    "name, " +
                                    "category_id, " +
                                    "slots, " +
                                    "time_period " +
                            "FROM " +
                                     "services " +
                            "WHERE " +
                                     "category_id = '" + id + "'";

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
                            S_CategoryId = reader["category_id"].ToString(),
                            S_Slots = reader["slots"].ToString(),
                            S_TimePeriod = reader["time_period"].ToString()
                        };
                        ServiceList.Add(service);
                        
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = ServiceList;
            return res;

        }

        public Response AddServiceDetails(GetServiceModal addService)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO services " +
                                          "(cost," +
                                           "description," +
                                           "name," +
                                           "category_id," +
                                           "slots," +
                                           "time_period) " +
                               "VALUES('" + addService.S_Cost + "'," +
                                       "'" + addService.S_Description + "'," +
                                       "'" + addService.S_Name + "'," +
                                       "'" + addService.S_CategoryId + "'," +
                                       "'" + addService.S_Slots + "'," +
                                       "'" + addService.S_TimePeriod + "')";



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