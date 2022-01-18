using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DangKiMonHoc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = HttpContext.Current.Server.GetLastError();
            if (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            if (ex is HttpException)
            {
                if (((HttpException)ex).GetHttpCode() == 404)
                {
                    Response.Redirect("~/PageNotFound.html");
                }
                else
                {
                    //Response.Redirect("~/PageError.html");
                }
            }

            HttpContext.Current.Server.ClearError();

        }
    }
}
