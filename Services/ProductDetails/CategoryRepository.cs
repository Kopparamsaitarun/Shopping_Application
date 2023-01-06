using Domain.EntityFramework;
using Domain.Model.Product;
using Services.ProductDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public void DeleteCategory(Category category)
        {
           dbContext.Categories.Remove(category);
        }

        public List<Category> GetAllCategories()
        {
            return dbContext.Categories.ToList();
        }

        public Category GetCategoryId(int id)
        {
            return dbContext.Categories.Where(x=>x.Id == id).FirstOrDefault(); 
        }

        public void InsertCategory(Category category)
        {
            dbContext.Categories.Add(category);
        }

        public void Save()
        {
           dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            dbContext.Categories.Update(category);
        }
    }
}
