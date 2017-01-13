using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPILab.Infrastructure.Attributes
{
    public class ATestAttribute : ActionFilterAttribute, IBaseAttribute
    {
        public int Order { get; set; }

        public ATestAttribute()
        {
            this.Order = 0;
        }

        public ATestAttribute(int Order)
        {
            this.Order = Order;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //base.OnActionExecuting(actionContext);
        }
    }

}