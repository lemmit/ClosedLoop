using ClosedLoop.Models;

namespace ClosedLoop.Web.ViewModels
{
    public class CategoryViewModel
    {
        public string CategoryPathUri { get; }
        public string ParentCategoryPathUri { get; }
        public string ParentCategoryName { get; }
        public Category Category { get; }

        public CategoryViewModel(string categoryPath, string parentCategoryPath, string parentCategoryName, Category category)
        {
            CategoryPathUri = categoryPath;
            ParentCategoryPathUri = parentCategoryPath;
            ParentCategoryName = parentCategoryName;
            Category = category;
        }
    }
}