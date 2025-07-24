using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Database_Layer;
using biZTrack.Static;

namespace WebApplication1.DataAccess
{
    public class DAVehicle : IVehicle
    {
        public Response AddVehicalDetails(GetVehicleModal addVehicle)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO vehicles " +
                                          "(color," +
                                           "license_plate," +
                                           "make," +
                                           "model," +
                                           "user_id," +
                                           "type," +
                                           "vehicle_no) " +
                               "VALUES('" + addVehicle.V_Color + "'," +
                                       "'" + addVehicle.V_LicensePlate + "'," +
                                       "'" + addVehicle.V_Make + "'," +
                                       "'" + addVehicle.V_Model + "'," +
                                       "'" + addVehicle.V_UserID + "'," +
                                       "'" + addVehicle.V_Type + "'," +
                                       "'" + addVehicle.V_No + "')";



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

        public Response getAllVehicles()
        {
            Response res = new Response();
            List<GetVehicleModal> VehicleList = new List<GetVehicleModal>();

            string Query = "SELECT " +
                                "id, " +
                                "color," +
                                "license_plate, " +
                                "make, " +
                                "model, " +
                                "user_id, " +
                                "type, " +
                                "vehicle_no " +
                            "FROM " +
                                "vehicles ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetVehicleModal Vehicle = new GetVehicleModal();


                        Vehicle.V_ID = reader["id"].ToString();
                        Vehicle.V_Color = reader["color"].ToString();
                        Vehicle.V_LicensePlate = reader["license_plate"].ToString();
                        Vehicle.V_Make = reader["make"].ToString();
                        Vehicle.V_Model = reader["model"].ToString();
                        Vehicle.V_UserID = reader["user_id"].ToString();
                        Vehicle.V_Type = reader["type"].ToString();
                        Vehicle.V_No = reader["vehicle_no"].ToString();

                        VehicleList.Add(Vehicle);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = VehicleList;
            return res;
        }

        public Response GetVehicleById(string id)
        {


            Response res = new Response();
            List<GetVehicleModal> VehicleList = new List<GetVehicleModal>();

            string Query = "SELECT " +
                                "id, " +
                                "color," +
                                "license_plate, " +
                                "make, " +
                                "model, " +
                                "user_id, " +
                                "type, " +
                                "vehicle_no " +
                            "FROM " +
                                "vehicles " +
                            "WHERE " +
                                "id = '" + id + "'";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetVehicleModal Vehicle = new GetVehicleModal();


                        Vehicle.V_ID = reader["id"].ToString();
                        Vehicle.V_Color = reader["color"].ToString();
                        Vehicle.V_LicensePlate = reader["license_plate"].ToString();
                        Vehicle.V_Make = reader["make"].ToString();
                        Vehicle.V_Model = reader["model"].ToString();
                        Vehicle.V_UserID = reader["user_id"].ToString();
                        Vehicle.V_Type = reader["type"].ToString();
                        Vehicle.V_No = reader["vehicle_no"].ToString();

                        VehicleList.Add(Vehicle);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = VehicleList;
            return res;
           
        }
        public Response GetVehicleByuserId(string id)
        {
            Response res = new Response();
            List<GetVehicleModal> VehicleList = new List<GetVehicleModal>();

            string Query = "SELECT " +
                                "id, " +
                                "color," +
                                "license_plate, " +
                                "make, " +
                                "model, " +
                                "user_id, " +
                                "type, " +
                                "vehicle_no " +
                            "FROM " +
                                "vehicles " +
                            "WHERE " +
                                "id = '" + id + "'";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetVehicleModal Vehicle = new GetVehicleModal();


                        Vehicle.V_ID = reader["id"].ToString();
                        Vehicle.V_Color = reader["color"].ToString();
                        Vehicle.V_LicensePlate = reader["license_plate"].ToString();
                        Vehicle.V_Make = reader["make"].ToString();
                        Vehicle.V_Model = reader["model"].ToString();
                        Vehicle.V_UserID = reader["user_id"].ToString();
                        Vehicle.V_Type = reader["type"].ToString();
                        Vehicle.V_No = reader["vehicle_no"].ToString();

                        VehicleList.Add(Vehicle);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = VehicleList;
            return res;

        }
    }
}