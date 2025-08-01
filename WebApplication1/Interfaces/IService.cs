using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IService
    {
        Response AddServiceDetails(GetServiceModal addService);
        Response GetAllServices();
        Response GetServiceByServiceID(string ServiceID);
        Response PutServiceDetails(GetServiceModal addService);
        Response DeleteServiceDetails(GetServiceModal addService);



    }
}
