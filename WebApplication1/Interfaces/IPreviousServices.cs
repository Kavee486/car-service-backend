using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IPreviousServices
    {
        Response GetAllPreviousServices();
        Response GetPreviousServicesByCustomerID(string CustomerID);
        Response AddPreviousService(GetPreviousServicesModal previousService);
        Response DeletePreviousService(string ServiceID);
    }
}
