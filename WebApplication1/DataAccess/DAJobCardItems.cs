using biZTrack.Static;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;


namespace WebApplication1.DataAccess
{
    public class DAJobCardItems: IJobCardItems
    {
        public Response AddJobCardItemsDetails(GetJobCardItemsModal addJobCardItems)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO JobCardItems " +
                                          "(JobCardID," +
                                           "ServiceID," +
                                           "PartID," +
                                           "Qty," +
                                           "Charge) " +
                               "VALUES('" + addJobCardItems.J_JobCardID + "'," +
                                       "'" + addJobCardItems.J_ServiceID + "'," +
                                       "'" + addJobCardItems.J_PartID + "'," +
                                       "'" + addJobCardItems.J_Qty + "'," +
                                       "'" + addJobCardItems.J_Charge + "')";



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

        public Response GetAllJobCardItems()

        {
            Response res = new Response();
            List<GetJobCardItemsModal> JobCardItemsList = new List<GetJobCardItemsModal>();

            string Query = "SELECT " +
                                "ItemID, " +
                                "JobCardID," +
                                "ServiceID, " +
                                "PartID," +
                                "Qty, " +
                                "Charge " +

                            "FROM " +
                                "JobCardItems ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetJobCardItemsModal JobCardItems = new GetJobCardItemsModal
                        {
                            J_ItemID = reader["ItemID"].ToString(),
                            J_JobCardID = reader["JobCardID"].ToString(),
                            J_ServiceID = reader["ServiceID"].ToString(),
                            J_PartID = reader["PartID"].ToString(),
                            J_Qty = reader["Qty"].ToString(),
                            J_Charge = reader["Charge"].ToString()
                        };


                        JobCardItemsList.Add(JobCardItems);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = JobCardItemsList;
            return res;


        }

        public Response GetJobCardItemsByJobCardID(string JobCardID)
        {
            Response res = new Response();
            List<GetJobCardItemsModal> JobCardItemsList = new List<GetJobCardItemsModal>();

            string Query = "SELECT " +
                                "ItemID, " +
                                "JobCardID," +
                                "ServiceID, " +
                                "PartID," +
                                "Qty, " +
                                "Charge " +
                            "FROM " +
                                     "JobCardItems " +
                            "WHERE " +
                                     "JobCardID = '" + JobCardID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetJobCardItemsModal JobCardItems = new GetJobCardItemsModal
                        {
                            J_ItemID = reader["ItemID"].ToString(),
                            J_JobCardID = reader["JobCardID"].ToString(),
                            J_ServiceID = reader["ServiceID"].ToString(),
                            J_PartID = reader["PartID"].ToString(),
                            J_Qty = reader["Qty"].ToString(),
                            J_Charge = reader["Charge"].ToString()
                        };
                        JobCardItemsList.Add(JobCardItems);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = JobCardItemsList;
            return res;
        }

        public Response GetJobCardItemsByServiceID(string ServiceID)
        {
            Response res = new Response();
            List<GetJobCardItemsModal> JobCardItemsList = new List<GetJobCardItemsModal>();

            string Query = "SELECT " +
                                "ItemID, " +
                                "JobCardID," +
                                "ServiceID, " +
                                "PartID," +
                                "Qty, " +
                                "Charge " +
                            "FROM " +
                                     "JobCardItems " +
                            "WHERE " +
                                     "ServiceID = '" + ServiceID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetJobCardItemsModal JobCardItems = new GetJobCardItemsModal
                        {
                            J_ItemID = reader["ItemID"].ToString(),
                            J_JobCardID = reader["JobCardID"].ToString(),
                            J_ServiceID = reader["ServiceID"].ToString(),
                            J_PartID = reader["PartID"].ToString(),
                            J_Qty = reader["Qty"].ToString(),
                            J_Charge = reader["Charge"].ToString()
                        };
                        JobCardItemsList.Add(JobCardItems);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = JobCardItemsList;
            return res;
        }
    }
}