using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPILab.Models;
using WebAPILab.Models.FakeDB;

namespace WebAPILab.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        public FakeDB db { get; set; }
        public ProductsController()
        {
            db = new FakeDB();
        }

        //GET: products
        [Route("")]
        public IQueryable<Product> GetProduct()
        {
            return db.Product.AsQueryable();
        }

        // GET: products/5
        [Route("{id:int=8}")]
        public IHttpActionResult GetProduct1(int id)
        {
            Product product = db.Product.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: products/search/Jack
        [Route("search/{name}")]
        public IHttpActionResult GetSearchProduct(string name)
        {
            Product product = db.Product.FirstOrDefault(p => p.ProductName.Contains(name));
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Route("add")]
        public IHttpActionResult PostProduct(Product product)
        {
            var isSuccess = db.Add(product);
            if (isSuccess == false)
            {
                return InternalServerError();
            }
            return Ok(db.Product);
        }

        [Route("update/{id:int}")]
        public IHttpActionResult PatchProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var isSuccess = db.update(id, product);
            if (isSuccess == false)
            {
                return NotFound();
            }

            Product item = db.Product.Where(x => x.Id == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [Route("delete/{id:int}")]
        public IHttpActionResult DeleteProduct(int id)
        {

            var isSuccess = db.Delete(id);
            if (isSuccess == false)
            {
                return NotFound();
            }

            return Ok(db.Product);
        }

        // POST api/test/5 (用 ~ 可以覆寫 RoutePrefix 設定)
        [Route("~/api/test/{id:int}")]
        public IHttpActionResult GetProduct2(int id)
        {
            Product product = db.Product.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Route("routeName/{id}", Name = "GetProductById", Order = 1)]
        public IHttpActionResult GetUrl(int id)
        {
            var uri = Url.Link("GetProductById", new { id = id });
            return Ok(uri);
        }

        [Route("~/v1/Now/{x:datetime}", Name = "GetNow1", Order = 1)]
        public IHttpActionResult GetNow1(DateTime x)
        {
            var uri = Url.Link("GetNow1", new { x = DateTime.Now });
            return Ok(uri);
        }

        [Route("~/v2/Now/{*x:datetime}", Name = "GetNow2", Order = 1)]
        public IHttpActionResult GetNow2(DateTime x)
        {
            var uri = Url.Link("GetNow2", new { x = DateTime.Now });
            return Ok(uri);
        }

        [Route("~/Demo/{age}/{xx}")]
        public IHttpActionResult GetDemo2(int age ,string xx , string name)
        {
            return Ok(new {age = age, xx = xx, name = name });
        }

        [Route("~/Demo2/{age}/{xx}")]
        public IHttpActionResult GetDemo2([FromUri]Demo2 p)
        {
            return Ok(new { FromUri = 'Y', age = p.age, xx = p.xx, name = p.name });
        }

        [Route("~/Demo3")]
        public IHttpActionResult PostDemo3(Demo2 p)
        {
            return Ok(new { age = p.age, xx = p.xx, name = $"{p.name}Demo3" });
        }

        [Route("~/Demo4")]
        public IHttpActionResult PostDemo4([FromBody]int id)
        {
            return Ok(new {id=id });
        }

        [Route("~/{a:int}/Demo5")]
        public IHttpActionResult PutDemo5(int a, Demo2 p)
        {
            return Ok(new {a = a, age = p.age, xx = p.xx, name = $"{p.name}Demo5" });
        }

        public class Demo2
        {
            public int age { get; set; }
            public string xx { get; set; }
            public string name { get; set; }
        }
    }


}

//[ResponseType(typeof(Product))]
