using StackOverflowClone.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.Repositories
{
    public interface ICategoriesRepository
    {
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryID);
        List<Category> GetCategories();
        List<Category> GetCategoryByCategoryID(int categoryID);
    }
    public class CategoriesRepository : ICategoriesRepository
    {
        StackOverflowDbContext db;
        public CategoriesRepository() { 
        db = new StackOverflowDbContext();
        }
        public void DeleteCategory(int categoryID)
        {
            Category deleteCategory  = db.Categories.Where(temp => temp.CategoryID == categoryID).FirstOrDefault();
            if(deleteCategory !=null)
            {
                db.Categories.Remove(deleteCategory);
                db.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoryByCategoryID(int categoryID)
        {
            
            List<Category> categories = db.Categories.Where(temp =>temp.CategoryID == categoryID).ToList(); 
            return categories;
        }

        public void InsertCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Category updateCategory = db.Categories.Where(temp =>temp.CategoryID == category.CategoryID).FirstOrDefault();
            if(updateCategory != null)
            {
                updateCategory.CategoryName = category.CategoryName;
                db.SaveChanges();
            }
        }
    }
}
