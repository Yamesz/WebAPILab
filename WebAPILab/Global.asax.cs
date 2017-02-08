using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

            // 限制只有本地端的電腦才能瀏覽特定的路徑.
            HttpContextBase httpContextBase = new HttpContextWrapper(httpApplication.Context);
            var isLocalIP = IsLocalIPAddress(httpContextBase);

            var requestPath = httpApplication.Request.Path.ToLower();
            var isSpecialRequest = this.CheckRequestPath(requestPath);

            if (isLocalIP == false
                && isSpecialRequest == true)
            {
                this.Context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                httpApplication.Context.Response.Redirect("http://www.yungching.com.tw/");
            }
        }

        /// <summary>
        /// 限制只有本地端的電腦才能瀏覽特定的路徑.
        /// </summary>
        /// <param name="requestPath">The request path.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool CheckRequestPath(string requestPath)
        {
            bool result = false;

            var coditions = new string[]
            {

                "/Help", // ASP.NET Web API Help Page
                "/NanoProfiler", // NanoProfiler
                "/Swagger", // Swagger
            };

            foreach (var item in coditions)
            {
                if (requestPath.Contains(item.ToLower()))
                {
                    return true;
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether [is local ip address] [the specified HTTP request].
        /// </summary>
        /// <param name="httpContextBase">The HTTP context base.</param>
        /// <returns><c>true</c> if [is local ip address] [the specified HTTP request]; otherwise, <c>false</c>.</returns>
        public static bool IsLocalIPAddress(HttpContextBase httpContextBase)
        {
            bool result = false;
            string ipAddress = httpContextBase.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            try
            {
                if (!string.IsNullOrEmpty(ipAddress) &&
                    ipAddress.ToUpper().IndexOf("UNKNOWN") < 0)
                {
                    result = CheckIPAddress(ipAddress);
                }
                if (!result && string.IsNullOrWhiteSpace(ipAddress))
                {
                    ipAddress = httpContextBase.Request.ServerVariables["REMOTE_ADDR"];
                    if (ipAddress.Equals("::1"))
                    {
                        // 127.0.0.1
                        return true;
                    }
                    result = CheckIPAddress(ipAddress);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private static bool CheckIPAddress(string ipAddress)
        {
            bool result = false;
            Regex reg = new Regex(@"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$");
            if (ipAddress.IndexOf(",") > -1 || ipAddress.IndexOf(";") > -1)
            {
                //有","或";"，估計為多代理。取第一個不是内網的IP。
                ipAddress = ipAddress.Replace('"', ' ').Replace(" ", "");
                string[] ipstr = ipAddress.Split(",;".ToCharArray());
                for (int i = 0; i < ipstr.Length; i++)
                {
                    if (reg.IsMatch(ipstr[i]) &&
                        ipstr[i].StartsWith("10.") ||
                        ipstr[i].StartsWith("192.168") ||
                        ipstr[i].StartsWith("172.16."))
                    {
                        result = true;
                        break;
                    }
                }
            }
            else
            {
                if (ipAddress.StartsWith("10.") ||
                    ipAddress.StartsWith("192.168") ||
                    ipAddress.StartsWith("172.16."))
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
