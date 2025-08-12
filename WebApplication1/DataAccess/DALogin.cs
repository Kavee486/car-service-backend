using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using BCrypt.Net;

namespace WebApplication1.DataAccess
{
    public class DALogin : ILogin
    {

        // Sign Up method
        public ApiResponse SignUp(string userName, string email, string phone, string password)
        {
            throw new NotImplementedException();
        }


        public OtpResponse Login(string contact)
        {
            OtpResponse res = new OtpResponse();
            List<GetLoginModel> UserList = new List<GetLoginModel>();

            string query = @"SELECT * FROM Users
                     WHERE MobileNo = '" + contact + "'";

            using (var DBconnect = new DBconnect())
            {
                using (SqlDataReader reader = DBconnect.ReadTable(query))
                {
                    while (reader.Read())
                    {
                        GetLoginModel User = new GetLoginModel
                        {
                            UserName = reader["UserName"].ToString(),
                            MobileNo = reader["MobileNo"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                        UserList.Add(User);
                    }
                }
            }

            // If exactly one user is found, generate OTP and send it via SMS
            if (UserList.Count == 1)
            {
                Random random = new Random();
                int otp = random.Next(10000, 99999);
                string message = $"Your OTP is: {otp}.";
                string smsApiUrl = $"https://esystems.cdl.lk/Backend/SMSGateway/api/SMS/DTSSendMessage?mobileNo={contact}&message={message}";

                // Send OTP via API
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(smsApiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        res.StatusCode = 200;
                        res.Result = $"OTP sent successfully to {contact}.";
                        res.OtpCode = otp;
                        res.UserName = UserList[0].UserName;  // Return the first user if only one is found
                        res.Email = UserList[0].Email;       // Return email in the response
                    }
                    else
                    {
                        res.StatusCode = 500;
                        res.Result = "Failed to send OTP via SMS Gateway.";
                    }
                }
            }
            else
            {
                res.StatusCode = 404;
                res.Result = "User not found or multiple users exist with this contact.";
            }

            return res;
        }


        // Verify OTP
        //public ApiResponse VerifyOtp(string contact, int otpCode)
        //{
        //    ApiResponse res = new ApiResponse();

        //    // Check if OTP exists for the contact and validate it
        //    if (otpStorage.ContainsKey(contact) && otpStorage[contact] == otpCode)
        //    {
        //        res.StatusCode = 200;
        //        res.Result = "OTP verified successfully. You can now log in.";
        //        otpStorage.Remove(contact);  // Remove OTP after successful verification
        //    }
        //    else
        //    {
        //        res.StatusCode = 400;
        //        res.Result = "Invalid OTP or OTP expired.";
        //    }

        //    return res;
        //}


    }


    }


//public OtpResponse Login(string contact)
//{
//    var result = new OtpResponse();
//    try
//    {
//        if (string.IsNullOrEmpty(contact))
//        {
//            result.StatusCode = 400;
//            result.Result = "Contact number is required.";
//            return result;
//        }

//        using (var connection = new SqlConnection(DBconnect))
//        {
//            connection.Open();

//            string query = @"
//                SELECT * FROM Users
//                WHERE MobileNo = @contact";

//            using (var command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@contact", contact);

//                using (var reader = command.ExecuteReader())
//                {
//                    if (reader.HasRows)
//                    {
//                        reader.Read(); // Move to the first row
//                        var user = new GetLoginModel
//                        {
//                            UserName = reader["UserName"].ToString(),
//                            MobileNo = reader["MobileNo"].ToString(),
//                            Email = reader["Email"].ToString()
//                        };

//                        // Generate OTP
//                        Random random = new Random();
//                        int otp = random.Next(10000, 99999);

//                        // Create the message with OTP
//                        string message = $"One time validation (OTP): {otp}. Use this to login to E-Wharf.";

//                        // Send OTP via SMS using the provided SMS Gateway API
//                        string smsApiUrl = $"https://esystems.cdl.lk/Backend/SMSGateway/api/SMS/DTSSendMessage?mobileNo={contact}&message={Uri.EscapeDataString(message)}";

//                        using (var client = new HttpClient())
//                        {
//                            var response = client.GetAsync(smsApiUrl).Result;
//                            if (response.IsSuccessStatusCode)
//                            {
//                                result.StatusCode = 200;
//                                result.Result = $"OTP sent successfully to {contact}.";
//                                result.OtpCode = otp;
//                                result.UserName = user.UserName;
//                                result.Email = user.Email; // Return email in the response
//                            }
//                            else
//                            {
//                                result.StatusCode = 500;
//                                result.Result = "Failed to send OTP via SMS Gateway.";
//                            }
//                        }
//                    }
//                    else
//                    {
//                        result.StatusCode = 404;
//                        result.Result = "User not found with this contact number.";
//                    }
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        result.StatusCode = 500;
//        result.Result = ex.Message;
//    }
//    return result;
//}



//public OtpResponse Login(string contact)
//{

//    OtpResponse res = new OtpResponse();
//    List<GetLoginModel> UserList = new List<GetLoginModel>();

//    string query = @"SELECT * FROM Users
//                     WHERE MobileNo = @contact"; 

//    using (var DBconnect = new DBconnect())
//    {
//        using (SqlDataReader reader = DBconnect.ReadTable(query))
//        {
//            while (reader.Read())
//            {

//                GetLoginModel User = new GetLoginModel
//                {
//                    UserName = reader["UserName"].ToString(),
//                    MobileNo = reader["MobileNo"].ToString(),
//                    Email = reader["Email"].ToString()
//                };
//                UserList.Add(User);
//            }
//        }
//    }
//    res.StatusCode = 200;
//    res.Result = UserList;
//    return res;
//}
