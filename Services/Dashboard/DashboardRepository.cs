using Domain.Model.Dashboard;
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
        private readonly IFileUploadService _fileUploadService;





        public DashboardRepository(IGenericRepository<Productlist> shoppingRepository, IFileUploadService fileUploadService)
        {
            this._genericRepository = shoppingRepository;
            _fileUploadService = fileUploadService;
        }
        public IEnumerable<Productlist> GetAllProduct()
        {

            List<Productlist> productlist = new List<Productlist>();
            _genericRepository.GetAll().ToList().ForEach(u =>
            {
                Productlist product = null;
                product = new Productlist()
                {
                    Id = u.Id,
                    ProductDiscription = u.ProductDiscription,
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
        public void InsertProduct(Productlist model)
        {
            string unique = _fileUploadService.Upload(model.ProductImage);

            Productlist entity = null;
            entity = new Productlist
            {
                ProductDiscription = model.ProductDiscription,
                ProductPrice = model.ProductPrice,
                ProductName = model.ProductName,
                ProductImage = model.ProductImage,
                InStock = model.InStock,
                InCart = model.InCart,
                Quantity = model.Quantity,

            };
            _genericRepository.Insert(entity);
        }


        public Productlist GetProduct(long id)
        {
            return _genericRepository.GetT(id);
        }

        public void DeleteProduct(long id)
        {


            Productlist product = GetProduct(id);
            _genericRepository.Remove(product);
            _genericRepository.Savechanges();
        }

    }
}
