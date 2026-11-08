using System.Web;
using System.Web.Mvc;

namespace _22DH114533_Buoi5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
