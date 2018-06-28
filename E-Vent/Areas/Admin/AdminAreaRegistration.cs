using System.Web.Mvc;

namespace E_Vent.Areas.Admin
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
                //new { action = "Index", id = UrlParameter.Optional }
                new { controller = "Cidade", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "E_Vent.Areas.Admin.Controllers" }
            );
        }
    }
}