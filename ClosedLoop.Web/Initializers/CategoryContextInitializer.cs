using System.Data.Entity;
using ClosedLoop.Data;
using ClosedLoop.Models;
using static ClosedLoop.Extensions.CategoryExtensions;

namespace ClosedLoop.Web.Initializers
{
    public class CategoryContextInitializer : 
                                                //DropCreateDatabaseAlways<CategoryContext>
                                                DropCreateDatabaseIfModelChanges<CategoryContext>
    {
        CategoryContext _categoryContext;
        protected override void Seed(CategoryContext context)
        {
            _categoryContext = context;

            var initialCategories =
                n("Root", 
                    n("Houses",
                        n("Flats",
                            n("A",
                                n("1"),
                                n("2"),
                                n("3"),
                                n("4",
                                    n("4-1"),
                                    n("4-2"),
                                    n("4-3",
                                        n("4-3-1"),
                                        n("4-3-2"),
                                        n("4-3-3"),
                                        n("4-3-4")
                                    ),
                                    n("4-4")
                                )
                            ),
                            n("B"),
                            n("C"),
                            n("D")
                        ),
                        n("Bungalows")
                    ),
                    n("Tickets",
                        n("Dogs"),
                        n("Cats")
                    ),
                    n("Games",
                        n("Hammer"),
                        n("Screwdriver")
                    )
               );
            RecursiveAdd(initialCategories);
            _categoryContext.SaveChanges();
        }

        private void RecursiveAdd(Category category)
        {
            _categoryContext.Categories.Add(category);
            foreach (var subCategory in category.SubCategories)
            {
                RecursiveAdd(subCategory);
            }
        }

    }
}

