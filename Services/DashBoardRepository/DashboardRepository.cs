using Domain.Model;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DashBoard
{
    public class DashboardRepository : IDashboardRepository
    {
        IGenericRepository<Productlst> _genericRepository;
        private readonly IFileUploadService _fileUploadService;





        public DashboardRepository(IGenericRepository<Productlst> shoppingRepository, IFileUploadService fileUploadService)
        {
            this._genericRepository = shoppingRepository;
            _fileUploadService = fileUploadService;
        }
        public IEnumerable<Productlst> GetAllProduct()
        {

            List<Productlst> productlist = new List<Productlst>();
            _genericRepository.GetAll().ToList().ForEach(u =>
            {
                Productlst product = null;
                product = new Productlst()
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
            IEnumerable<Productlst> products = productlist;

            return products;


        }
        public void InsertProduct(Productlst model)
        {
            string unique = _fileUploadService.Upload(model.ProductImage);

            Productlst entity = null;
            entity = new Productlst
            {
                ProductDescription = model.ProductDescription,
                ProductPrice = model.ProductPrice,
                ProductName = model.ProductName,
                ProductImage = unique,
                InStock = model.InStock,
                InCart = model.InCart,
                Quantity = model.Quantity,

            };
            _genericRepository.Insert(entity);
        }


        public Productlst GetProduct(long id)
        {
            return _genericRepository.GetT(id);
        }

        public void DeleteProduct(long id)
        {


            Productlst product = GetProduct(id);
            _genericRepository.Remove(product);
            _genericRepository.Savechanges();
        }

    }
}
