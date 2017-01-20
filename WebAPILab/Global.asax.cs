using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebAPILab.Infrastructure.Attributes;

namespace WebAPILab
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //=============================================================
            //實作Attribute Order
            GlobalConfiguration.Configuration.Services.Add(
                typeof(System.Web.Http.Filters.IFilterProvider), new CustomFilterProvider());
            var providers = GlobalConfiguration.Configuration.Services.GetFilterProviders();
            var defaultprovider = providers.First(i => i is ActionDescriptorFilterProvider);
            GlobalConfiguration.Configuration.Services.Remove(
               typeof(System.Web.Http.Filters.IFilterProvider),
               defaultprovider);
            //=============================================================

            //移除XML Formatter
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication httpApplication = sender as HttpApplication;
            if (httpApplication != null 
                && httpApplication.Context != null)
            {
                httpApplication.Context.Response.Headers.Remove("Server");
                httpApplication.Context.Response.Headers.Remove("X-AspNet-Version");
                
            }
        }
    }
}
