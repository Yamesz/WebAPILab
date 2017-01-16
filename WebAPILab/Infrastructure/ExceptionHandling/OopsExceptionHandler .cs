using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace WebAPILab.Infrastructure.ExceptionHandling
{
    public class OopsExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;
            context.Result = new TextPlainErrorResult(context.ExceptionContext.Request, exception);
        }

        public class TextPlainErrorResult : IHttpActionResult
        {
            public Exception Exception { get; private set; }
            public HttpRequestMessage Request { get; private set; }

            public TextPlainErrorResult(
               HttpRequestMessage request,
               Exception exception)
            {
                this.Exception = exception;
                this.Request = request;
            }


            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var errorContent = new JObject()
            {
                new JProperty("status","ERROR" ),
                new JProperty("message", Exception.Message)
            };

                //HttpResponseMessage response =
                //                 new HttpResponseMessage(HttpStatusCode.InternalServerError);
                //response.Content = errorContent;
                //response.RequestMessage = Request;
                var a = this.Request.CreateResponse(HttpStatusCode.InternalServerError, errorContent);
                return Task.FromResult(a);
            }
        }
    }
}