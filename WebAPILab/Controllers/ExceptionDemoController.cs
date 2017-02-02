using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPILab.Controllers
{
    public class ExceptionDemoController : ApiController
    {
        [Route("Exception/NotImplementedException")]
        public IHttpActionResult GetNotImplementedException()
        {
            throw new NotImplementedException("This method is not implemented");
        }

        [Route("Exception/DivideByZeroException")]
        public IHttpActionResult GetDivideByZeroException()
        {
            int a = 0;
            var b = 1 / a;
            return Ok(b);
        }
    }
}
