using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebAPILab.Infrastructure.Attributes;

namespace WebAPILab.Controllers
{
    [RoutePrefix("products")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [ATest(Order = 2)]
        [BTest(Order =1)]
        public IEnumerable<Tuple<string, string>> Get()
        {
            IHttpActionSelector actionSelector =
                this.Configuration.Services.GetActionSelector();

            HttpActionDescriptor actionDescriptor =
                actionSelector.SelectAction(this.ControllerContext);

            foreach (FilterInfo filterInfo in actionDescriptor.GetFilterPipeline())
            {
                yield return new Tuple<string, string>(
                    filterInfo.Instance.GetType().Name,
                    filterInfo.Scope.ToString()
                );
            }
        }

        // GET api/values/5
        [Route("products/{id:int}")]
        public string Get(int id)
        {
            if(id == 4)
            {
                throw new Exception("當id=4就會產生Exception");
            }
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
