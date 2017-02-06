using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPILab.Models.FakeDB
{
    public class FakeDB
    {
        private List<Product> _product;
        public FakeDB()
        {
            this._product = new List<Product>()
            {
                new Product() { Id = 1, ProductName = "a" },
                new Product() { Id = 2, ProductName = "b" },
                new Product() { Id = 3, ProductName = "c" },
                new Product() { Id = 4, ProductName = "d" },
                new Product() { Id = 5, ProductName = "e" },
                new Product() { Id = 6, ProductName = "f" },
                new Product() { Id = 7, ProductName = "g" },
                new Product() { Id = 8, ProductName = "h" },
                new Product() { Id = 9, ProductName = "i" },
                new Product() { Id = 10, ProductName = "j" },
                new Product() { Id = 87, ProductName = "八七" },
            };
        }
        public IEnumerable<Product> Product
        {
            get { return this._product; }
        }

        public bool update(int id, Product product)
        {
            if (id != product.Id)
            {
                return false;
            }

            var item = this._product.Where(x => x.Id == id).FirstOrDefault();
            if (item == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(product.ProductName) == false)
            {
                item.ProductName = product.ProductName;
            }

            return true;
        }

        public bool Delete(int id)
        {
            var itemToRemove = this._product.SingleOrDefault(x => x.Id == id);
            if(itemToRemove == null)
            {
                return false;
            }

            
            return this._product.Remove(itemToRemove);
        }

        public bool Add(Product product)
        {
            if(product == null)
            {
                return false;
            }
            this._product.Add(product);
            return true;
        }
    }
}