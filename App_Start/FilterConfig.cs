using System.Web;
using System.Web.Mvc;

namespace Examen_2_Aplicacion
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
