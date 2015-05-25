using System.Web;
using System.Web.Mvc;

namespace conti.maurizio.GoogleMapsMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
