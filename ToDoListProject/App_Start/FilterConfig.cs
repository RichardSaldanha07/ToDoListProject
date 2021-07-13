using System.Web;
using System.Web.Mvc;

namespace ToDoListProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //Allows Authorization throughout the Application
            filters.Add(new AuthorizeAttribute());
        }
    }
}
