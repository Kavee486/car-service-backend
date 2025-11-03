using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GetTechniciansModal
    {


        public string TechnicianID { get; set; }  // To store the Technician's ID
        public string FullName { get; set; }      // To store the Technician's Full Name
        public string Phone { get; set; }         // To store the Technician's Phone
        public string Email { get; set; }         // To store the Technician's Email
        public string Address { get; set; }       // To store the Technician's Address (can be NULL)
        public string NIC { get; set; }           // To store the Technician's NIC
        public string Status { get; set; }        // To store the Technician's Status



    }
}