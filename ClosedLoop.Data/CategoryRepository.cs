using System.Linq;
using ClosedLoop.Models;
using System.Data.Entity;
using System;
using ClosedLoop.Repositories;

namespace ClosedLoop.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly CategoryContext _categoryContext;
        public CategoryRepository()
        {
            _categoryContext = new CategoryContext();
        }

        public CategoryRepository(string connectionName, bool forceInitialize = false)
        {
            _categoryContext = new CategoryContext(connectionName);
            _categoryContext.Database.Initialize(forceInitialize);
        }


        public Category GetRootCategory()
        {
            return _categoryContext.Categories
                .Include(c => c.SubCategories)
                .Single(c => c.ParentCategoryId == null);
        }

        public Category GetCategory(string categoryName = null)
        {
            return _categoryContext
                .Categories
                .Include(c => c.SubCategories)
                .Include(c => c.ParentCategory)
                .Single(category => category.Name == categoryName);
        }

        public void Clear()
        {
            _categoryContext.Database.Delete();
        }

        public void Add(Category category)
        {
            _categoryContext.Categories.Add(category);
        }

        public void Save()
        {
            _categoryContext.SaveChanges();
        }
        public void Dispose()
        {
            _categoryContext.Dispose();
        }
    }
}