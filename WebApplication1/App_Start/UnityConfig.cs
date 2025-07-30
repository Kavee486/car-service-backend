using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApplication1.DataAccess;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register your interfaces and implementations 
            container.RegisterType<ITest, DATest>();
            container.RegisterType<IUser, DAUser>();
            container.RegisterType<IVehicle, DAVehicle>();
            container.RegisterType<IService, DAService>();
            container.RegisterType<IServiceCategory, DAServiceCategory>();
            container.RegisterType<IAppointment, DAAppointment>();
            container.RegisterType<IPartsInventory, DAPartsInventory>();
            container.RegisterType<ICustomer, DACustomer>();
            container.RegisterType<IJobCardItems, DAJobCardItems>();
            container.RegisterType<IInvoices, DAInvoices>();
            container.RegisterType<IBookings, DABookings>();
            // Set the dependency resolver for MVC
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
