using System.Web;
using System.Web.Mvc;

namespace conti.maurizio._5i.WebDatabase
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
