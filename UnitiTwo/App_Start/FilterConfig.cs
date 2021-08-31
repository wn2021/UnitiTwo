using System.Web;
using System.Web.Mvc;
using UnitiTwo.Controllers;

namespace UnitiTwo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthenticationAttribute());
        }
    }
}
