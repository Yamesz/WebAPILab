using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace WebAPILab.Infrastructure.Routing
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        public string AllowedVersion { get; private set; }

        public VersionConstraint(string allowedVersion)
        {
            this.AllowedVersion = allowedVersion.ToLower();
        }

        /// <summary>
        /// 判斷此執行個體是否等於指定的路由。
        /// </summary>
        /// <param name="request">要求。</param>
        /// <param name="route">要比較的路由。</param>
        /// <param name="parameterName">參數名稱。</param>
        /// <param name="values">參數值清單。</param>
        /// <param name="routeDirection">路由方向。</param>
        /// <returns>如果這個執行個體等於指定的路由，則為 true，否則為 false。</returns>
        public bool Match(
            HttpRequestMessage request,
            IHttpRoute route,
            string parameterName,
            IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            object obj;
            if (values.TryGetValue(parameterName, out obj) == false || obj == null)
            {
                return false;
            }
            return this.AllowedVersion.Equals(obj.ToString().ToLower());
        }
    }
}