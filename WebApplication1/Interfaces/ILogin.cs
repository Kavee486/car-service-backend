using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ILogin
    {
        OtpResponse Login(string contact); // Method to generate OTP and send via SMS
        //ApiResponse UpdatePreparationComplete(string year, string vno); // Placeholder for other methods
        ApiResponse VerifyOtp(string contact, int otpCode); // For OTP verification
        //ApiResponse LoginWithCredentials(string username, string password); // For username/password validation

        //ApiResponse SignUp(string username, string password, string email); // For username/password validation


    }
}
