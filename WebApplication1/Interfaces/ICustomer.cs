using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ICustomer
    {
        Response AddCustomerDetails(GetCustomerModal addCustomer);
        Response GetAllCustomers();
        Response GetCustomerByCustomerID(string CustomerID);
        Response PutCustomerDetails(GetCustomerModal addCustomer);
        Response DeleteCustomerDetails(GetCustomerModal addCustomer);
        Response ActivateCustomerDetails(GetCustomerModal addCustomer);
    }
}
