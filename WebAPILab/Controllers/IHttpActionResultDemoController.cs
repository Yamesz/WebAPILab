using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static WebAPILab.Controllers.ProductsController;

namespace WebAPILab.Controllers
{
    public class IHttpActionResultDemoController : ApiController
    {
        [Route("IHttpActionResult/Ok")]
        public IHttpActionResult GetDemo1()
        {
            //200
            return Ok(new { test = "ok" });
        }

        [Route("IHttpActionResult/BadRequest")]
        public IHttpActionResult GetDemo2()
        {
            //400
            return BadRequest("Bad Request");
        }

        [Route("IHttpActionResult/NotFound")]
        public IHttpActionResult GetDemo3()
        {
            //404
            return NotFound();
        }

        [Route("IHttpActionResult/Created")]
        public IHttpActionResult GetDemo4()
        {
            //201
            return Created<Demo2>("llll", new Demo2());
        }

        [Route("IHttpActionResult/InternalServerError")]
        public IHttpActionResult GetDemo5()
        {
            //500
            return InternalServerError(new Exception("意外意外"));
        }

        [Route("IHttpActionResult/Content")]
        public IHttpActionResult GetDemo6()
        {
            //404
            return Content(HttpStatusCode.NotFound,"逆天專用");
        }

        [Route("IHttpActionResult/Content2")]
        public IHttpActionResult GetDemo7()
        {
            //410
            return Content<Demo2>(HttpStatusCode.Gone, new Demo2());
        }

    }
}
