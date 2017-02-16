using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebAPILab.Infrastructure.Modules
{
    public class FooModule : IHttpModule
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
            Debug.WriteLine("FooModule - BeginRequest");
       
        }

        private void context_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication httpApplication = sender as HttpApplication;
            Debug.WriteLine("FooModule - PreSendRequestHeaders");
           
        }

      

        public void Dispose()
        {
            
        }

       
    }
}