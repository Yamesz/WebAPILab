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
      
        [Route("")]
        public IQueryable<Product> GetProduct()
        {
            return db.Product.AsQueryable();
        }

        // GET: products/search/Will
        [Route("search/{name}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetSearchProduct(string name)
        {
            Product product = db.Product.FirstOrDefault(p => p.ProductName.Contains(name));
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: products/5
        [Route("{id:int}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Product.Where(x => x.Id == id).FirstOrDefault();
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
            if(isSuccess == false)
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
    }
}
