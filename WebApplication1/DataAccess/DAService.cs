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
                                "ServiceID, " +
                                "ServiceName," +
                                "Description, " +
                                "BaseCharge " +

                            "FROM " +
                                "services " +
                            "WHERE " +
                                "Status = 'A' ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetServiceModal service = new GetServiceModal
                        {
                            S_ServiceID = reader["ServiceID"].ToString(),
                            S_ServiceName = reader["ServiceName"].ToString(),
                            S_Description = reader["Description"].ToString(),
                            S_BaseCharge = reader["BaseCharge"].ToString()
                        };


                        ServiceList.Add(service);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = ServiceList;
            return res;


        }

        public Response GetServiceByServiceID(string ServiceID)
        {

            Response res = new Response();
            List<GetServiceModal> ServiceList = new List<GetServiceModal>();

            string Query = "SELECT " +
                                "ServiceID, " +
                                "ServiceName," +
                                "Description, " +
                                "BaseCharge " +
                            "FROM " +
                                     "services " +
                            "WHERE " +
                                     "ServiceID = '" + ServiceID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetServiceModal service = new GetServiceModal
                        {
                            S_ServiceID = reader["ServiceID"].ToString(),
                            S_ServiceName = reader["ServiceName"].ToString(),
                            S_Description = reader["description"].ToString(),
                            S_BaseCharge = reader["BaseCharge"].ToString()
                        };
                        ServiceList.Add(service);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = ServiceList;
            return res;
        }
 
        public Response PutServiceDetails(GetServiceModal addService)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE services
                                        SET ServiceName = '" + addService.S_ServiceName + @"',
                                            Description = '" + addService.S_Description + @"',
                                            BaseCharge = '" + addService.S_BaseCharge + @"'
                                        WHERE ServiceID = '" + addService.S_ServiceID + @"'";
             

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
        public Response DeleteServiceDetails(GetServiceModal addService)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string updateQuery = @" UPDATE services
                                        SET Status = 'I'
                                        WHERE ServiceID = '" + addService.S_ServiceID + @"'";
             

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

        public Response AddServiceDetails(GetServiceModal addService)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO services " +
                                          "(ServiceName," +
                                           "Description," +
                                           "Status," +
                                           "BaseCharge) " +
                               "VALUES('" + addService.S_ServiceName + "'," +
                                       "'" + addService.S_Description + "'," +
                                       "'A'," +
                                       "'" + addService.S_BaseCharge + "')";



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