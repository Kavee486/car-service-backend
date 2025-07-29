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
    public class DAPartsInventory: IPartsInventory
    {
        public Response AddPartInventoryDetails(GetPartsInventoryModal addPartsInventory)
        {
            Response res = new Response();
            DBconnect DBconnect = new DBconnect();
            try
            {
                string Query = "INSERT INTO PartsInventory " +
                                          "(PartName," +
                                           "StockQty," +
                                           "UnitPrice) " +
                               "VALUES('" + addPartsInventory.P_PartName + "'," +
                                       "'" + addPartsInventory.P_StockQty + "'," +
                                       "'" + addPartsInventory.P_UnitPrice + "')";



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

        public Response getAllPartsInventory()
        {

        
            Response res = new Response();
            List<GetPartsInventoryModal> PartsInventoryList = new List<GetPartsInventoryModal>();

            string Query = "SELECT " +
                                "PartID, " +
                                "PartName," +
                                "StockQty, " +
                                "UnitPrice " +
                            "FROM " +
                                "PartsInventory ";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetPartsInventoryModal PartsInventory = new GetPartsInventoryModal
                        {
                            P_PartID = reader["PartID"].ToString(),
                            P_PartName = reader["PartName"].ToString(),
                            P_StockQty = reader["StockQty"].ToString(),
                            P_UnitPrice = reader["UnitPrice"].ToString()
                        };


                       PartsInventoryList.Add(PartsInventory);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = PartsInventoryList;
            return res;


        }

        public Response GetPartsInventoryByPartID(string PartID)
        {


            Response res = new Response();
            List<GetPartsInventoryModal> PartsInventoryList = new List<GetPartsInventoryModal>();

            string Query = "SELECT " +
                                "PartID, " +
                                "PartName," +
                                "StockQty, " +
                                "UnitPrice " +
                            "FROM " +
                                     "PartsInventory " +
                            "WHERE " +
                                     "PartID = '" + PartID + "'";
            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(Query))
                {
                    while (reader.Read())
                    {

                        GetPartsInventoryModal service = new GetPartsInventoryModal
                        {
                            P_PartID = reader["PartID"].ToString(),
                            P_PartName = reader["PartName"].ToString(),
                            P_StockQty = reader["StockQty"].ToString(),
                            P_UnitPrice = reader["UnitPrice"].ToString()
                        };
                        PartsInventoryList.Add(service);
                    }
                }
            }
            res.StatusCode = 200;
            res.ResultSet = PartsInventoryList;
            return res;
        }

    }
}