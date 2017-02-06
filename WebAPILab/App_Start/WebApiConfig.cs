using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.ExceptionHandling;
using WebAPILab.Infrastructure.ExceptionHandling;
using WebAPILab.Infrastructure.ActionFilters;
using WebAPILab.Infrastructure.Handlers;

namespace WebAPILab
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //加入自訂DelegatingHandler
            config.MessageHandlers.Add(new SampleHandler("Server A", 2));
            config.MessageHandlers.Add(new SampleHandler("Server B", 4));

            //Global Error Handling
            config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());

            //加入自訂ExceptionFilterAttribute
            GlobalConfiguration.Configuration.Filters.Add(new NotImplExceptionFilterAttribute());
 
            // Web API 設定和服務
            // 將 Web API 設定成僅使用 bearer 權杖驗證。
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
