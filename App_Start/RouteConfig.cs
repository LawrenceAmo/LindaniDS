using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lindaniDS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "none",
               url: "",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Register",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Register", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "CreateCourse",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Courses", action = "Create", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "learnerBook",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Learners", action = "create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LearnerInfo",
                url: "{controller}/{action}/{email}",
                defaults: new { controller = "Learners", action = "myBookings", email = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "LicenceInfo",
                url: "{controller}/{action}/{email}",
                defaults: new { controller = "Licences", action = "myBookings", email = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "LicenceBook",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Licences", action = "create", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "LicenceDetails",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Licences", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Hire",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "VehicleHires", action = "ViewCar", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "registerCar",
                url: "{controller}/{action}",
                defaults: new { controller = "VehicleHires", action = "Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AddCar",
                url: "{controller}/{action}",
                defaults: new { controller = "VehicleHires", action = "Add", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "adminLearners",
                url: "{controller}/{action}",
                defaults: new { controller = "Learners", action = "AdminView", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "adminLicence",
                url: "{controller}/{action}",
                defaults: new { controller = "Licences", action = "AdminView", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Pay",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Payments", action = "Create", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Logout",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Logout", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "AllBookings",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "AllBookings", id = UrlParameter.Optional }
          );
           

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
