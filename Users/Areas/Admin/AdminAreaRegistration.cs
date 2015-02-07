using System.Web.Mvc;

namespace Users.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Users.Areas.Admin.Controllers"}
            );
            context.MapRoute(
               "Admin_default2",
               "Admin",
               new { action = "Index", controller="DashBoard", id = UrlParameter.Optional },
               namespaces: new[] { "Users.Areas.Admin.Controllers" }
           );
        }
       
    }
}