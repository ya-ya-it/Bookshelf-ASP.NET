using System.Web;
using System.Web.Mvc;

namespace Bookshelf_assignment2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // force ssl
            //My ssl is not working so Im testing on local mashine without it
            //filters.Add(new RequireHttpsAttribute());
        }
    }
}
