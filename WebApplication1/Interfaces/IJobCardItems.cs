using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IJobCardItems
    {
        Response GetAllJobCardItems();
        Response GetJobCardItemsByJobCardID(string JobCardID);
        Response GetJobCardItemsByServiceID(string ServiceID);
        Response AddJobCardItemsDetails(GetJobCardItemsModal addJobCardItems);



    }
}
