using Domain.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductDetails
{
    public interface ICategory
    {
        List<Category> GetAllCategories();
        Category GetCategoryId(int id); 
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        void Save();

    }
}
