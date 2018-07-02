using System.Web;
using System.Web.Mvc;

namespace MyCqrs1.Client.Host.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
