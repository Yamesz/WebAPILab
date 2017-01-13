using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPILab.Infrastructure.Attributes
{
    public class BTestAttribute : ActionFilterAttribute, IBaseAttribute
    {
        public int Order { get; set; }

        public BTestAttribute()
        {
            this.Order = 0;
        }

        public BTestAttribute(int Order)
        {
            this.Order = Order;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //base.OnActionExecuting(actionContext);
        }
    }

}