using Domain.Model;
using Domain.Model.Dashboard;
using Domain.Model.User;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        IGenericRepository<Productlist> _genericRepository;
        public DashboardRepository(IGenericRepository<Productlist> shoppingRepository)
        {
            this._genericRepository = shoppingRepository;
        }

        /// <summary>
        /// Get all the product from the table by using genericrepository 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Productlist> GetAllProduct()
        {

            List<Productlist> productlist = new List<Productlist>();
            _genericRepository.GetAll().ToList().ForEach(u =>
            {
                Productlist product = null;
                product = new Productlist()
                {
                    Id = u.Id,
                    ProductDescription = u.ProductDescription,
                    ProductPrice = u.ProductPrice,
                    ProductName = u.ProductName,
                    ProductImage = u.ProductImage,
                    InStock = u.InStock,
                    InCart = u.InCart,
                    Quantity = u.Quantity,
                };
                productlist.Add(product);
            });
            IEnumerable<Productlist> products = productlist;

            return products;

        }
        
        /// <summary>
        /// Get the product by Low to high by using linq statement 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Productlist> GetProductLowHigh()
        {

            IEnumerable<Productlist> productlist = GetAllProduct();
            IEnumerable<Productlist> productsLH = productlist.OrderBy(x => x.ProductPrice);
            return productsLH;
        }

        /// <summary>
        /// Get the product by high to low by using linq statement 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Productlist> GetProductHighLow()
        {

            IEnumerable<Productlist> productlist = GetAllProduct();
            IEnumerable<Productlist> productsLH = productlist.OrderByDescending(x => x.ProductPrice);
            return productsLH;
        }
        /// <summary>
        /// Insert the product in the table 
        /// </summary>
        /// <param name="model"></param>
        public void InsertProduct(ProductlistModel model)
        {
            
            Productlist entity = null;
            entity = new Productlist
            {
                ProductDescription = model.ProductDiscription,
                ProductPrice = model.ProductPrice,
                ProductName = model.ProductName,
                ProductImage = model.ProductImage,
                InStock = model.InStock,
                InCart = model.InCart,
                Quantity = model.Quantity,

            };
            _genericRepository.Insert(entity);
        }

        /// <summary>
        /// Get Particular product by passing ID fro that calling generic repository 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Productlist GetProduct(long id)
        {
            return _genericRepository.GetT(id);
        }

        /// <summary>
        /// Authorize person can remove the product from the table
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(long id)
        {
            Productlist product = GetProduct(id);
            _genericRepository.Remove(product);
            _genericRepository.Savechanges();
        }

        /// <summary>
        /// Add the item in cart by passing ID 
        /// </summary>
        /// <param name="id"></param>
        public void AddtoCart(long id)
        {
            Productlist product = GetProduct(id);
            product.InCart = true;
            _genericRepository.Update(product);

        }
        /// <summary>
        /// Retriving all the product from the table and add alues in list 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Productlist> IDashboardRepository.GetAllProduct()
        {
            List<Productlist> productlist = new List<Productlist>();
            _genericRepository.GetAll().ToList().ForEach(u =>
            {
                Productlist product = null;
                product = new Productlist()
                {
                    Id = u.Id,
                    ProductDescription = u.ProductDescription,
                    ProductPrice = u.ProductPrice,
                    ProductName = u.ProductName,
                    ProductImage = u.ProductImage,
                    InStock = u.InStock,
                    InCart = u.InCart,
                    Quantity = u.Quantity,
                };
                productlist.Add(product);
            });
            IEnumerable<Productlist> products = productlist;

            return products;

        }

        /// <summary>
        /// Search the product by Passing the search string which check conatins in product name and description
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<Productlist> GetSearchItems(string searchString)
        {
            List<Productlist> productlist = new List<Productlist>();
            _genericRepository.GetAll().ToList().ForEach(u =>
            {
                Productlist product = null;
                product = new Productlist()
                {
                    Id = u.Id,
                    ProductDescription = u.ProductDescription,
                    ProductPrice = u.ProductPrice,
                    ProductName = u.ProductName,
                    ProductImage = u.ProductImage,
                    InStock = u.InStock,
                    InCart = u.InCart,
                    Quantity = u.Quantity,
                };
                if (product.ProductName.Contains(searchString.ToUpper()) || product.ProductDescription.Contains(searchString.ToUpper()))
                {
                    productlist.Add(product);
                }
            });
            IEnumerable<Productlist> products =  productlist;
            return products;
        }
      
    }
}
