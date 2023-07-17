using AutoMapper;
using StackOverflowClone.DomainModels;
using StackOverflowClone.Repositories;
using StackOverflowClone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowClone.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);
        void DeleteCategory(int categoryID);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoryByCategoryID(int categoryID);
    }
    public class CategoriesService : ICategoriesService
    {
        ICategoriesRepository categoriesRepository;
        public CategoriesService()
        {
            categoriesRepository = new CategoriesRepository();
        }
        public void InsertCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel, Category>(cvm);
            categoriesRepository.InsertCategory(category);
        }
        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Category category = mapper.Map<CategoryViewModel,Category>(cvm);
            categoriesRepository.UpdateCategory(category);
        }
        public void DeleteCategory(int categoryID)
        {
            categoriesRepository.DeleteCategory(categoryID);
        }
        public List<CategoryViewModel> GetCategories()
        {
            List<Category> categories = categoriesRepository.GetCategories();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(categories);
            return cvm;
        }
        public CategoryViewModel GetCategoryByCategoryID(int categoryID)
        {
            Category category = categoriesRepository.GetCategoryByCategoryID(categoryID).FirstOrDefault();
            CategoryViewModel cvm = null;
            if (category != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Category, CategoryViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                cvm =  mapper.Map<Category,CategoryViewModel>(category);
            }
            return cvm;
        }
    }
}
