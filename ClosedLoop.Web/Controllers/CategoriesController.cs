using System.Linq;
using System.Web.Mvc;
using ClosedLoop.Data;
using ClosedLoop.Models;
using ClosedLoop.Repositories;
using ClosedLoop.Web.Infrastructure;
using ClosedLoop.Web.ViewModels;

namespace ClosedLoop.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryRepository db = new CategoryRepository();

        // GET: Categories
        public ActionResult Index(string categories = null)
        {
            var categoryPath = SplitCategoryPath(categories);

            var category = IsCategorySpecified(categoryPath)
                ? db.GetCategory(categoryPath.Last())
                : db.GetRootCategory();

            var categoryViewModel = new CategoryViewModel(
                    string.Join("/", categoryPath),
                    string.Join("/", categoryPath.WithoutLast()),
                    category.ParentCategory?.Name ?? "Root",
                    category
                );

            return View(categoryViewModel);
        }

        private static string[] SplitCategoryPath(string categories)
        {
            return categories?.Split('/') ?? new string[] { };
        }

        private static bool IsCategorySpecified(string[] categories)
        {
            return categories != null && categories.Length > 0 && !string.IsNullOrEmpty(categories.Last());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
