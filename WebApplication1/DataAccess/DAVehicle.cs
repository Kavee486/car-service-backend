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
                                          "(CustomerID," +
                                           "PlateNumber," +
                                           "make," +
                                           "model," +
                                           "Year," +
                                           "VIN) " +
                               "VALUES('" + addVehicle.V_CustomerID + "'," +
                                       "'" + addVehicle.V_PlateNumber + "'," +
                                       "'" + addVehicle.V_Make + "'," +
                                       "'" + addVehicle.V_Model + "'," +
                                       "'" + addVehicle.V_Year + "'," +
                                       "'" + addVehicle.V_VIN + "')";



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
                                "VehicleID, " +
                                "CustomerID," +
                                "PlateNumber, " +
                                "make, " +
                                "model, " +
                                "Year, " +
                                "VIN " +
                            "FROM " +
                                "vehicles ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetVehicleModal Vehicle = new GetVehicleModal();


                        Vehicle.V_VehicleID = reader["VehicleID"].ToString();
                        Vehicle.V_CustomerID = reader["CustomerID"].ToString();
                        Vehicle.V_PlateNumber = reader["PlateNumber"].ToString();
                        Vehicle.V_Make = reader["make"].ToString();
                        Vehicle.V_Model = reader["model"].ToString();
                        Vehicle.V_Year = reader["Year"].ToString();
                        Vehicle.V_VIN = reader["VIN"].ToString();

                        VehicleList.Add(Vehicle);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = VehicleList;
            return res;
        }

        public Response GetVehicleByCustomerID(string CustomerID)
        {


            Response res = new Response();
            List<GetVehicleModal> VehicleList = new List<GetVehicleModal>();

            string Query = "SELECT " +
                                "VehicleID, " +
                                "CustomerID," +
                                "PlateNumber, " +
                                "make, " +
                                "model, " +
                                "Year, " +
                                "VIN " +
                            "FROM " +
                                "vehicles " +
                            "WHERE " +
                                "CustomerID = '" + CustomerID + "'";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetVehicleModal Vehicle = new GetVehicleModal();


                        Vehicle.V_VehicleID = reader["VehicleID"].ToString();
                        Vehicle.V_CustomerID = reader["CustomerID"].ToString();
                        Vehicle.V_PlateNumber = reader["PlateNumber"].ToString();
                        Vehicle.V_Make = reader["make"].ToString();
                        Vehicle.V_Model = reader["model"].ToString();
                        Vehicle.V_Year = reader["Year"].ToString();
                        Vehicle.V_VIN = reader["VIN"].ToString();

                        VehicleList.Add(Vehicle);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = VehicleList;
            return res;
           
        }
        public Response GetVehicleByVehicleID(string VehicleID)
        {
            Response res = new Response();
            List<GetVehicleModal> VehicleList = new List<GetVehicleModal>();

            string Query = "SELECT " +
                                "VehicleID, " +
                                "CustomerID," +
                                "PlateNumber, " +
                                "make, " +
                                "model, " +
                                "Year, " +
                                "VIN " +
                            "FROM " +
                                "vehicles " +
                            "WHERE " +
                                "VehicleID = '" + VehicleID + "'";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetVehicleModal Vehicle = new GetVehicleModal();


                        Vehicle.V_VehicleID = reader["VehicleID"].ToString();
                        Vehicle.V_CustomerID = reader["CustomerID"].ToString();
                        Vehicle.V_PlateNumber = reader["PlateNumber"].ToString();
                        Vehicle.V_Make = reader["make"].ToString();
                        Vehicle.V_Model = reader["model"].ToString();
                        Vehicle.V_Year = reader["Year"].ToString();
                        Vehicle.V_VIN = reader["VIN"].ToString();

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