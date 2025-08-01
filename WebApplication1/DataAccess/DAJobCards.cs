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

















        public Response GetAllJobCards()

        {
            Response res = new Response();
            List<GetJobCardsModal> JobCardsList = new List<GetJobCardsModal>();

            string Query = "SELECT " +
                                "JobCardID, " +
                                "BookingID," +
                                "CreatedDate, " +
                                "Technician, " +
                                "Status " +
                            "FROM " +
                                "JobCards ";

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
                            J_Status = reader["Status"].ToString()
                        };


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
                                "Status " +
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
                            J_Status = reader["Status"].ToString()
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
                string updateQuery = @" UPDATE PartsInventory
                                        SET Technician = '" + addJobCards.J_Technician + @"',
                                            Status = '" + addJobCards.J_Status + @"'
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
    }
}