using System.Web;
using System.Web.Mvc;
using static System.Web.Mvc.HandleErrorInfo;

namespace E_Vent
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
