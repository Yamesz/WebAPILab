using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using static WebAPILab.Controllers.ProductsController;

namespace WebAPILab.Controllers
{
    public class HttpResponseMessageDemoController : ApiController
    {
        [Route("HttpResponseMessage/Demo1")]
        public HttpResponseMessage GetDemo1()
        {
            var response = Request.CreateResponse<Demo2>(
                    HttpStatusCode.Created,
                    new Demo2(),
                    new JsonMediaTypeFormatter() { Indent = true }, "text/json");
            response.ReasonPhrase = "YC";
            
            return response;
        }
    }
}
