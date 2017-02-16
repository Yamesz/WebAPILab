using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebAPILab.Infrastructure.Modules
{
    public class RequestProcessModule : IHttpModule
    {
        private HttpApplication Context { get; set; }

        public void Init(HttpApplication context)
        {
            this.Context = context;

            context.BeginRequest +=
                new EventHandler(this.context_BeginRequest);

            context.PreSendRequestHeaders +=
                new EventHandler(this.context_PreSendRequestHeaders);
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication httpApplication = sender as HttpApplication;
            Debug.WriteLine("context_BeginRequest");
            this.RestrictRequestPath(httpApplication);
        }

        private void context_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication httpApplication = sender as HttpApplication;
            Debug.WriteLine("context_PreSendRequestHeaders");
            this.RestrictRequestPath(httpApplication);
        }

        private void RestrictRequestPath(HttpApplication httpApplication)
        {
            HttpContextBase httpContextBase = new HttpContextWrapper(httpApplication.Context);
        
            var requestPath = httpApplication.Request.Path.ToLower();
            Debug.WriteLine($"requestPath = {requestPath}");
        }

        public void Dispose()
        {
            
        }

       
    }
}