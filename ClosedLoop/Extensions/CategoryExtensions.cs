using ClosedLoop.Models;

namespace ClosedLoop.Extensions
{
    public static class CategoryExtensions
    {
        public static Category n(int id, string name, params Category[] subcategories)
        {
            return new Category(id, name, subcategories);
        }

        public static Category n(string name, params Category[] subcategories)
        {
            return new Category(0, name, subcategories);
        }
    }
}