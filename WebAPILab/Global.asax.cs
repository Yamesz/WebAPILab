using Newtonsoft.Json.Serialization;
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

            //取得 JsonFormatter
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //輸出 Camel Casing 格式 (不改變 Model 定義)
            //json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //Json.NET 預設會序列化為本地時間，也可改成預設序列化為 UTC 時間
            //json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
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
