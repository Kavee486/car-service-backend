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
    public class DAAppointment : IAppointment
    {
        private string date;
        private string service_id;

        public GetAppointmentModal Appointment { get; private set; }

        public Response GetAllAppointments()
        {
            Response res = new Response();
            List<GetAppointmentModal> AppointmentList = new List<GetAppointmentModal>();

            string Query = "SELECT " +
                                "id, " +
                                "date," +
                                "time, " +
                                "technician_id, " +
                                "vehicle_id, " +
                                "approved, " +
                                "end_time, " +
                                "start_time, " +
                                "service_id, " +
                                "user_id " +

                            "FROM " +
                                "appointments ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetAppointmentModal appointment = new GetAppointmentModal
                        {
                            A_ID = reader["id"].ToString(),
                            A_Date = reader["date"].ToString(),
                            A_Time = reader["time"].ToString(),
                            A_TechnicianId = reader["technician_id"].ToString(),
                            A_VehicleId = reader["vehicle_id"].ToString(),
                            A_Approved = reader["approved"].ToString(),
                            A_EndTime = reader["end_time"].ToString(),
                            A_StartTime = reader["start_time"].ToString(),
                            A_ServiceId = reader["service_id"].ToString(),
                            A_UserId = reader["user_id"].ToString()
                        };


                        AppointmentList.Add(appointment);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = AppointmentList;
            return res;


        }

        public Response GetAppointmentsById(string id)
        {
            Response res = new Response();
            List<GetAppointmentModal> AppointmentList = new List<GetAppointmentModal>();

            string Query = "SELECT " +
                                "id, " +
                                "date," +
                                "time, " +
                                "technician_id, " +
                                "vehicle_id, " +
                                "approved, " +
                                "end_time, " +
                                "start_time, " +
                                "service_id, " +
                                "user_id " +

                            "FROM " +
                                     "appointments " +
                            "WHERE " +
                                     "id = '" + id + "'";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetAppointmentModal appointment = new GetAppointmentModal
                        {
                            A_ID = reader["id"].ToString(),
                            A_Date = reader["date"].ToString(),
                            A_Time = reader["time"].ToString(),
                            A_TechnicianId = reader["technician_id"].ToString(),
                            A_VehicleId = reader["vehicle_id"].ToString(),
                            A_Approved = reader["approved"].ToString(),
                            A_EndTime = reader["end_time"].ToString(),
                            A_StartTime = reader["start_time"].ToString(),
                            A_ServiceId = reader["service_id"].ToString(),
                            A_UserId = reader["user_id"].ToString()
                        };


                        AppointmentList.Add(appointment);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = AppointmentList;
            return res;


        }

        public Response GetAvailableTimeSlots(string date, string serviceId)
        {
            Response res = new Response();
            List<GetAppointmentModal> AppointmentList = new List<GetAppointmentModal>();

            string Query = "SELECT " +
                                "id, " +
                                "date," +
                                "time, " +
                                "technician_id, " +
                                "vehicle_id, " +
                                "approved, " +
                                "end_time, " +
                                "start_time, " +
                                "service_id, " +
                                "user_id " +

                            "FROM " +
                                     "appointments " +
                            "WHERE " +
                                     "date = '" + date + "' AND service_id = '" + service_id + "'";

            using (DBconnect DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetAppointmentModal appointment = new GetAppointmentModal
                        {
                            A_ID = reader["id"].ToString(),
                            A_Date = reader["date"].ToString(),
                            A_Time = reader["time"].ToString(),
                            A_TechnicianId = reader["technician_id"].ToString(),
                            A_VehicleId = reader["vehicle_id"].ToString(),
                            A_Approved = reader["approved"].ToString(),
                            A_EndTime = reader["end_time"].ToString(),
                            A_StartTime = reader["start_time"].ToString(),
                            A_ServiceId = reader["service_id"].ToString(),
                            A_UserId = reader["user_id"].ToString()
                        };


                        AppointmentList.Add(appointment);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = AppointmentList;
            return res;


        }

        public Response AddAppointmentDetails(GetAppointmentModal addAppointment)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO appointments " +
                                          "(date," +
                                           "time," +
                                           "technician_id," +
                                           "vehicle_id," +
                                           "approved," +
                                           "end_time," +
                                           "start_time," +
                                           "service_id," +
                                           "user_id) " +
                               "VALUES('" + addAppointment.A_Date + "'," +
                                       "'" + addAppointment.A_Time + "'," +
                                       "'" + addAppointment.A_TechnicianId + "'," +
                                       "'" + addAppointment.A_VehicleId + "'," +
                                       "'" + addAppointment.A_Approved + "'," +
                                       "'" + addAppointment.A_EndTime + "'," +
                                       "'" + addAppointment.A_StartTime + "'," +
                                       "'" + addAppointment.A_ServiceId + "'," +
                                       "'" + addAppointment.A_UserId + "')";



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