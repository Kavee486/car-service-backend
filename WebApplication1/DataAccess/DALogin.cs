using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Database_Layer;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DALogin : ILogin
    {
        // OTP storage - Consider using a more robust storage like Redis in production
        private static Dictionary<string, int> otpStorage = new Dictionary<string, int>();
        private static Dictionary<string, DateTime> otpExpiry = new Dictionary<string, DateTime>();

        // -------------------- SIGN UP --------------------
        public ApiResponse SignUp(string userName, string email, string phone, string password)
        {
            throw new NotImplementedException();
        }

        // -------------------- LOGIN (Generate OTP) --------------------
        //public OtpResponse Login(string contact)
        //{
        //    OtpResponse res = new OtpResponse();
        //    List<GetLoginModel> UserList = new List<GetLoginModel>();

        //    try
        //    {
        //        // Clean up expired OTPs first
        //        CleanupExpiredOTPs();

        //        // Validate contact number format
        //        if (string.IsNullOrWhiteSpace(contact))
        //        {
        //            res.StatusCode = 400;
        //            res.Result = "Contact number is required.";
        //            return res;
        //        }

        //        // Clean the contact number (remove spaces, special characters)
        //        contact = CleanPhoneNumber(contact);

        //        // Use your existing DBconnect method (keeping original structure but safer)
        //        string query = @"SELECT 
        //                        * FROM Users WHERE MobileNo = '" + contact + "'";

        //        using (var DBconnect = new DBconnect())
        //        {
        //            using (SqlDataReader reader = DBconnect.ReadTable(query))
        //            {
        //                while (reader.Read())
        //                {
        //                    GetLoginModel User = new GetLoginModel
        //                    {
        //                        UserName = reader["UserName"]?.ToString() ?? "",
        //                        MobileNo = reader["MobileNo"]?.ToString() ?? "",
        //                        RoleID = Convert.ToInt32(reader["RoleID"] ?? 0),
        //                        UserID = Convert.ToInt32(reader["UserID"] ?? 0),
        //                        Email = reader["Email"]?.ToString() ?? ""

        //                    };
        //                    UserList.Add(User);
        //                }
        //            }
        //        }

        //        // If exactly one user is found, generate OTP and send SMS
        //        if (UserList.Count == 1)
        //        {
        //            Random random = new Random();
        //            int otp = random.Next(100000, 999999); // 6-digit OTP for better security

        //            // Save OTP temporarily
        //            otpStorage[contact] = otp;
        //            otpExpiry[contact] = DateTime.Now.AddMinutes(5); // OTP valid for 5 minutes

        //            // Send SMS synchronously
        //            var smsResult = SendSMS(contact, otp);

        //            if (smsResult.Success)
        //            {
        //                res.StatusCode = 200;
        //                res.Result = $"OTP sent successfully to {contact}";
        //                res.OtpCode = otp; // Remove this in production for security
        //                res.RoleID = UserList[0].RoleID;
        //                res.UserID = UserList[0].UserID;
        //                res.UserName = UserList[0].UserName;
        //                res.Email = UserList[0].Email;
        //            }
        //            else
        //            {
        //                res.StatusCode = 500;
        //                res.Result = $"Failed to send OTP. Error: {smsResult.ErrorMessage}";

        //                // Remove OTP if SMS failed
        //                if (otpStorage.ContainsKey(contact))
        //                    otpStorage.Remove(contact);
        //                if (otpExpiry.ContainsKey(contact))
        //                    otpExpiry.Remove(contact);
        //            }
        //        }
        //        else if (UserList.Count == 0)
        //        {
        //            res.StatusCode = 404;
        //            res.Result = "User not found with this contact number.";
        //        }
        //        else
        //        {
        //            res.StatusCode = 409;
        //            res.Result = "Multiple users exist with this contact number.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res.StatusCode = 500;
        //        res.Result = $"An error occurred: {ex.Message}";

        //        // Log the exception details
        //        System.Diagnostics.Debug.WriteLine($"Login Error: {ex}");
        //    }

        //    return res;
        //}
        public OtpResponse Login(string contact)
        {
            OtpResponse res = new OtpResponse();
            List<GetLoginModel> UserList = new List<GetLoginModel>();

            try
            {
                // Clean up expired OTPs first
                CleanupExpiredOTPs();

                // Validate contact number format
                if (string.IsNullOrWhiteSpace(contact))
                {
                    res.StatusCode = 400;
                    res.Result = "Contact number is required.";
                    return res;
                }

                // Clean the contact number (remove spaces, special characters)
                contact = CleanPhoneNumber(contact);

                // ✅ Modified Query: join Users with Customers to get CustomerID
                string query = @"
            SELECT 
                u.UserID,
                u.UserName,
                u.RoleID,
                u.MobileNo,
                u.Email,
                c.CustomerID
            FROM Users u
            LEFT JOIN Customers c ON u.MobileNo = c.Phone
            WHERE u.MobileNo = @contact";

                using (var DBconnect = new DBconnect())
                {
                    using (SqlConnection conn = DBconnect.GetOpenConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@contact", contact);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    GetLoginModel user = new GetLoginModel
                                    {
                                        UserID = Convert.ToInt32(reader["UserID"]),
                                        UserName = reader["UserName"]?.ToString() ?? "",
                                        RoleID = Convert.ToInt32(reader["RoleID"]),
                                        MobileNo = reader["MobileNo"]?.ToString() ?? "",
                                        Email = reader["Email"]?.ToString() ?? "",
                                        // ✅ Add CustomerID field
                                        CustomerID = reader["CustomerID"] != DBNull.Value
                                            ? Convert.ToInt32(reader["CustomerID"])
                                            : 0
                                    };

                                    UserList.Add(user);
                                }
                            }
                        }
                    }
                }

                // If exactly one user is found, generate OTP and send SMS
                if (UserList.Count == 1)
                {
                    Random random = new Random();
                    int otp = random.Next(100000, 999999); // 6-digit OTP

                    // Save OTP temporarily
                    otpStorage[contact] = otp;
                    otpExpiry[contact] = DateTime.Now.AddMinutes(5); // valid 5 min

                    // Send SMS synchronously
                    var smsResult = SendSMS(contact, otp);

                    if (smsResult.Success)
                    {
                        res.StatusCode = 200;
                        res.Result = $"OTP sent successfully to {contact}";
                        res.OtpCode = otp; // ⚠️ for dev only, remove in production
                        res.RoleID = UserList[0].RoleID;
                        res.UserID = UserList[0].UserID;
                        res.UserName = UserList[0].UserName;
                        res.Email = UserList[0].Email;
                        // ✅ Add this line
                        res.CustomerID = UserList[0].CustomerID;
                    }
                    else
                    {
                        res.StatusCode = 500;
                        res.Result = $"Failed to send OTP. Error: {smsResult.ErrorMessage}";

                        otpStorage.Remove(contact);
                        otpExpiry.Remove(contact);
                    }
                }
                else if (UserList.Count == 0)
                {
                    res.StatusCode = 404;
                    res.Result = "User not found with this contact number.";
                }
                else
                {
                    res.StatusCode = 409;
                    res.Result = "Multiple users exist with this contact number.";
                }
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                res.Result = $"An error occurred: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Login Error: {ex}");
            }

            return res;
        }


        // -------------------- SEND SMS --------------------
        private (bool Success, string ErrorMessage) SendSMS(string contact, int otp)
        {
            try
            {
                string message = $"Your OTP for login is: {otp}. Valid for 5 minutes. Do not share this code.";
                string encodedMessage = HttpUtility.UrlEncode(message);

                // Format phone number for the API (ensure it has country code)
                string formattedNumber = FormatPhoneNumberForAPI(contact);

                string smsApiUrl = $"https://esystems.cdl.lk/Backend/SMSGateway/api/SMS/DTSSendMessage?mobileNo={formattedNumber}&message={encodedMessage}";

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30); // Set timeout

                    var response = client.GetAsync(smsApiUrl).Result; // Use .Result for synchronous call
                    string responseContent = response.Content.ReadAsStringAsync().Result;

                    System.Diagnostics.Debug.WriteLine($"SMS API Response: {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        // You might want to check the actual response content to ensure the SMS was sent
                        // Different SMS gateways return different success indicators
                        return (true, null);
                    }
                    else
                    {
                        return (false, $"HTTP {response.StatusCode}: {responseContent}");
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                return (false, $"Network error: {httpEx.Message}");
            }
            catch (TaskCanceledException tcEx)
            {
                return (false, "Request timeout. Please try again.");
            }
            catch (Exception ex)
            {
                return (false, $"SMS sending failed: {ex.Message}");
            }
        }

        // -------------------- PHONE NUMBER UTILITIES --------------------
        private string CleanPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber;

            // Remove all non-digit characters except +
            return System.Text.RegularExpressions.Regex.Replace(phoneNumber, @"[^\d+]", "");
        }

        private string FormatPhoneNumberForAPI(string phoneNumber)
        {
            // Clean the number first
            phoneNumber = CleanPhoneNumber(phoneNumber);

            // If it doesn't start with country code, add Sri Lankan country code
            if (!phoneNumber.StartsWith("+94") && !phoneNumber.StartsWith("94"))
            {
                // Remove leading 0 if present and add country code
                if (phoneNumber.StartsWith("0"))
                    phoneNumber = "94" + phoneNumber.Substring(1);
                else
                    phoneNumber = "94" + phoneNumber;
            }
            else if (phoneNumber.StartsWith("+94"))
            {
                phoneNumber = phoneNumber.Substring(1); // Remove + sign
            }

            return phoneNumber;
        }

        // -------------------- CLEANUP EXPIRED OTPS --------------------
        private void CleanupExpiredOTPs()
        {
            var expiredKeys = new List<string>();
            foreach (var kvp in otpExpiry)
            {
                if (kvp.Value < DateTime.Now)
                {
                    expiredKeys.Add(kvp.Key);
                }
            }

            foreach (var key in expiredKeys)
            {
                otpStorage.Remove(key);
                otpExpiry.Remove(key);
            }
        }

        // -------------------- VERIFY OTP --------------------
        public ApiResponse VerifyOtp(string contact, int otpCode)
        {
            ApiResponse res = new ApiResponse();

            try
            {
                // Clean the contact number
                contact = CleanPhoneNumber(contact);

                // Check if OTP exists for this contact
                if (!otpStorage.ContainsKey(contact))
                {
                    res.StatusCode = 400;
                    res.Result = "No OTP found for this contact. Please request a new OTP.";
                    return res;
                }

                // Check if OTP matches
                if (otpStorage[contact] != otpCode)
                {
                    res.StatusCode = 400;
                    res.Result = "Invalid OTP. Please check and try again.";
                    return res;
                }

                // Check expiry
                if (otpExpiry.ContainsKey(contact) && otpExpiry[contact] < DateTime.Now)
                {
                    res.StatusCode = 400;
                    res.Result = "OTP expired. Please request a new OTP.";

                    // Clean up expired OTP
                    otpStorage.Remove(contact);
                    otpExpiry.Remove(contact);
                    return res;
                }

                // OTP is valid, fetch user details
                string query = @"SELECT * FROM Users WHERE MobileNo = '" + contact + "'";
                GetLoginModel user = null;

                using (var DBconnect = new DBconnect())
                {
                    using (SqlDataReader reader = DBconnect.ReadTable(query))
                    {
                        if (reader.Read())
                        {
                            user = new GetLoginModel
                            {
                                UserID = Convert.ToInt32(reader["UserID"] ?? 0),
                                UserName = reader["UserName"]?.ToString() ?? "",
                                MobileNo = reader["MobileNo"]?.ToString() ?? "",
                                RoleID = Convert.ToInt32(reader["RoleID"] ?? 0),
                                Email = reader["Email"]?.ToString() ?? ""
                            };
                        }
                    }
                }

                if (user != null)
                {
                    // Set session
                    HttpContext.Current.Session["User"] = user;
                    HttpContext.Current.Session.Timeout = 30;

                    res.StatusCode = 200;
                    res.Result = "Login successful!";

                    // Clean up OTP after successful verification
                    otpStorage.Remove(contact);
                    otpExpiry.Remove(contact);
                }
                else
                {
                    res.StatusCode = 404;
                    res.Result = "User not found.";
                }
            }
            catch (Exception ex)
            {
                res.StatusCode = 500;
                res.Result = $"An error occurred during OTP verification: {ex.Message}";

                // Log the exception
                System.Diagnostics.Debug.WriteLine($"OTP Verification Error: {ex}");
            }

            return res;
        }
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
