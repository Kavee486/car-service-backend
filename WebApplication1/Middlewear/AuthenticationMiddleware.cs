using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.MiddleWear
{
    public class AuthenticationMiddleware : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userSession = HttpContext.Current.Session["User"];
            if (userSession == null)
            {
                // Check if the request is for login or OTP verification (allow these without authentication)
                string actionName = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                if (!(controllerName.Equals("Login", StringComparison.OrdinalIgnoreCase) &&
                      (actionName.Equals("Index", StringComparison.OrdinalIgnoreCase) ||
                       actionName.Equals("VerifyOtp", StringComparison.OrdinalIgnoreCase) ||
                       actionName.Equals("Login", StringComparison.OrdinalIgnoreCase))))
                {
                    // Redirect to login page if user is not authenticated
                    filterContext.Result = new RedirectResult("/Login/Index");
                }
            }
            else
            {
                // Check if user has access to the requested controller based on role
                var user = (GetLoginModel)HttpContext.Current.Session["User"];
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                if (!HasAccess(user.RoleID, controllerName))
                {
                    // Redirect to unauthorized page or appropriate dashboard
                    filterContext.Result = new RedirectResult(GetDashboardUrl(user.RoleID));
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private bool HasAccess(int roleId, string controllerName)
        {
            switch (roleId)
            {
                case 1: // Customer
                    return controllerName.Equals("Customer", StringComparison.OrdinalIgnoreCase);
                case 2: // Admin
                    return controllerName.Equals("Admin", StringComparison.OrdinalIgnoreCase);
                case 3: // Technician
                    return controllerName.Equals("Technician", StringComparison.OrdinalIgnoreCase);
                default:
                    return false;
            }
        }

        private string GetDashboardUrl(int roleId)
        {
            switch (roleId)
            {
                case 1: return "/Customer/Dashboard";
                case 2: return "/Admin/Dashboard";
                case 3: return "/Technician/Dashboard";
                default: return "/Login/Index";
            }
        }
    }
}