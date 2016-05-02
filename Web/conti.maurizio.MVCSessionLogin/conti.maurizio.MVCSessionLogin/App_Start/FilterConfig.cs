using System.Web;
using System.Web.Mvc;

namespace conti.maurizio.MVCSessionLogin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
