using Domain.EntityFramework;
using Domain.Model.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductDetails
{

    public class ProductRepository : IProduct
    {
        private readonly ApplicationDbContext dbContext;

        public ProductRepository(ApplicationDbContext Context)
        {
            dbContext = Context;
        }

        public void DeleteProduct(Product product)
        {
            dbContext.Products.Remove(product);
        }

        public List<Product> GetAllProduct()
        {
            return dbContext.Products.ToList();
        }

        public Product GetProductId(int Id)
        {
            return dbContext.Products.Include(x => x.Categories).ThenInclude(y => y.category).Where(a => a.Id == Id).FirstOrDefault();
        }

        public void InsertProduct(Product product)
        {
            dbContext.Products.Add(product);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            dbContext.Products.Update(product);
        }
    }
}
 
