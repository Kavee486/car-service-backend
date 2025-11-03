using biZTrack.Static;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DATimeslots : ITimeslots
    {
        public Response GetAllTimeslots()
        {
            Response res = new Response();
            List<GetTimeslotModal> TimeslotList = new List<GetTimeslotModal>();

            string Query = @"SELECT 
                                timeslotID, 
                                date, 
                                start_time, 
                                end_time, 
                                max_customers, 
                                status 
                             FROM Timeslot";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {
                        GetTimeslotModal slot = new GetTimeslotModal
                        {
                            T_TimeslotID = reader["timeslotID"].ToString(),
                            T_Date = reader["date"].ToString(),
                            T_StartTime = reader["start_time"].ToString(),
                            T_EndTime = reader["end_time"].ToString(),
                            T_MaxCustomers = reader["max_customers"].ToString(),
                            T_Status = reader["status"].ToString()
                        };
                        TimeslotList.Add(slot);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = TimeslotList;
            return res;
        }

        public Response GetTimeslotByID(string timeslotID)
        {
            Response res = new Response();
            List<GetTimeslotModal> TimeslotList = new List<GetTimeslotModal>();

            string Query = @"SELECT 
                                timeslotID, 
                                date, 
                                start_time, 
                                end_time, 
                                max_customers, 
                                status 
                             FROM Timeslot
                             WHERE timeslotID = '" + timeslotID + "'";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {
                        GetTimeslotModal slot = new GetTimeslotModal
                        {
                            T_TimeslotID = reader["timeslotID"].ToString(),
                            T_Date = reader["date"].ToString(),
                            T_StartTime = reader["start_time"].ToString(),
                            T_EndTime = reader["end_time"].ToString(),
                            T_MaxCustomers = reader["max_customers"].ToString(),
                            T_Status = reader["status"].ToString()
                        };
                        TimeslotList.Add(slot);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = TimeslotList;
            return res;
        }

        public Response AddTimeslot(GetTimeslotModal addTimeslot)
        {
            Response res = new Response();
            try
            {
                string Query = @"INSERT INTO Timeslot 
                                (date, start_time, end_time, max_customers, status)
                                VALUES (
                                    '" + addTimeslot.T_Date + @"',
                                    '" + addTimeslot.T_StartTime + @"',
                                    '" + addTimeslot.T_EndTime + @"',
                                    '" + addTimeslot.T_MaxCustomers + @"',
                                    'A')";

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(Query))
                    {
                        res.StatusCode = 200;
                        res.Result = "Timeslot added successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Failed to add timeslot!";
            }
            return res;
        }

        public Response UpdateTimeslot(GetTimeslotModal updateTimeslot)
        {
            Response res = new Response();
            try
            {
                string Query = @"UPDATE Timeslot
                                 SET 
                                    date = '" + updateTimeslot.T_Date + @"',
                                    start_time = '" + updateTimeslot.T_StartTime + @"',
                                    end_time = '" + updateTimeslot.T_EndTime + @"',
                                    max_customers = '" + updateTimeslot.T_MaxCustomers + @"'
                                 WHERE timeslotID = '" + updateTimeslot.T_TimeslotID + "'";

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(Query))
                    {
                        res.StatusCode = 200;
                        res.Result = "Timeslot updated successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Update failed!";
            }
            return res;
        }

        public Response DeleteTimeslot(GetTimeslotModal deleteTimeslot)
        {
            Response res = new Response();
            try
            {
                string Query = @"UPDATE Timeslot 
                                 SET status = 'I'
                                 WHERE timeslotID = '" + deleteTimeslot.T_TimeslotID + "'";

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(Query))
                    {
                        res.StatusCode = 200;
                        res.Result = "Timeslot deactivated successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Delete failed!";
            }
            return res;
        }

        public Response  ActivateTimeslot(GetTimeslotModal activateTimeslot)
        {
            Response res = new Response();
            try
            {
                string Query = @"UPDATE Timeslot 
                                 SET status = 'A'
                                 WHERE timeslotID = '" + activateTimeslot.T_TimeslotID + "'";

                using (var dbConnect = new DBconnect())
                {
                    if (dbConnect.AddEditDel(Query))
                    {
                        res.StatusCode = 200;
                        res.Result = "Timeslot activated successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.WriteToLog(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                res.StatusCode = 500;
                res.Result = "Activation failed!";
            }
            return res;
        }
    }
}
