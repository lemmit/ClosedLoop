using System.Collections.Generic;

namespace ClosedLoop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public Category ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Category> SubCategories { get; set; }


        public Category()
        {
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
            SubCategories = Empty;
        }

        public Category(int id, string name, ICollection<Category> subCategories)
        {
            Id = id;
            Name = name;
            SubCategories = subCategories;
            foreach (var subCategory in SubCategories)
            {
                subCategory.ParentCategory = this;
                subCategory.ParentCategoryId = Id;
            }
        }

        public static ICollection<Category> Empty => new List<Category>();
    }
}