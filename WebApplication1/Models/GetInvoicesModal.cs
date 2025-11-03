using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class GetInvoicesModal
    {
        public string I_InvoiceID { get; set; }
        public string I_JobCardID { get; set; }
        public string I_InvoiceDate { get; set; }
        public string I_TotalAmount { get; set; }
        public string I_PaymentStatus { get; set; }
        public string I_Status { get; set; }
        public string J_BookingID { get;  set; }
        public string J_JobCardStatus { get;  set; }
        public string JobCardID { get;  set; }
    }
}