using System;
using System.Data.Entity;
using ClosedLoop.Data;
using ClosedLoop.Exceptions;
using ClosedLoop.Models;
using ClosedLoop.Repositories;
using Machine.Specifications;
using static ClosedLoop.Extensions.CategoryExtensions;

namespace ClosedLoopTests.CategoryRepositorySpecs
{
    public abstract class When_requesting_category
    {        
        Establish context = () =>
        {
            TestAllCategories =
                n(1, "Root",
                    n(2, TestCategoryName,
                        n(5, "Test1.1"),
                        n(6, "Test1.2")
                    ),
                    n(3, "Test2"),
                    n(4, "Test3")
                );

            TestCategory = 
                n(2, TestCategoryName,
                    n(5, "Test1.1"),
                    n(6, "Test1.2")
                );
            CreateFreshRepository();
        };

        Cleanup after = () =>
        {
            CategoryRepository.Dispose();
        };

        private static void CreateFreshRepository()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<CategoryContext>());
            CategoryRepository = new CategoryRepository("TestDb", true);
            //initialize with data
            RecursiveAdd(TestAllCategories);
            CategoryRepository.Save();
        }

        private static void RecursiveAdd(Category category)
        {
            CategoryRepository.Add(category);
            foreach (var subCategory in category.SubCategories)
            {
                RecursiveAdd(subCategory);
            }
        }

        protected static ICategoryRepository CategoryRepository;
        protected static Category Category;
        protected static Category TestAllCategories;
        protected static Category TestCategory;
        internal const string TestCategoryName = "Test1";
        internal const string NonExistentCategoryName = "non_existent_category_123412fqwfd";

    }

    [Subject(typeof(ICategoryRepository))]
    public class When_category_is_unspecified
         : When_requesting_category
    {

        Because of = () =>
        {
            All = CategoryRepository.GetRootCategory();
        };

        It should_return_all_categories = () =>
        {
            
            All.ShouldEqual(TestAllCategories);
        };

        static Category All;
    }

    [Subject(typeof(ICategoryRepository))]
    public class When_requesting_some_category
        : When_requesting_category
    {
        Because of = () =>
        {
            Category = CategoryRepository.GetCategory(TestCategoryName);
        };

        It should_return_category_with_specified_name = () =>
        {
            Category.Name.ShouldEqual(TestCategoryName);
        };

        It should_have_children = () =>
        {
            Category.SubCategories.ShouldNotBeEmpty();
        };
    }

    [Subject(typeof(ICategoryRepository))]
    public class When_leaf_category_requested
        : When_requesting_category
    {
        Because of = () =>
        {
            LeafCategory = CategoryRepository.GetCategory("Test1.1");
        };

        It should_have_no_subcategories = () =>
        {
            LeafCategory.SubCategories.ShouldBeEmpty();
        };

        It should_have_name = () =>
        {
            LeafCategory.Name.ShouldNotBeEmpty();
            LeafCategory.Name.ShouldNotBeNull();
        };

        It should_have_parent = () =>
        {
            LeafCategory.ParentCategoryId.ShouldNotBeNull();
            LeafCategory.ParentCategory.ShouldNotBeNull();
        };

        static Category LeafCategory;
    }

    [Subject(typeof(ICategoryRepository))]
    public class When_nonleaf_category_requested
        : When_requesting_category
    {
        Because of = () =>
        {
            Exception = Catch.Exception(() =>
            {
                Category =
                    CategoryRepository.GetCategory(NonExistentCategoryName);
            });
        };

        It should_throw_category_not_found_exception => () =>
        {
            Exception.ShouldBeOfExactType<CategoryNotFoundException>();
        };

        static Exception Exception;
    }
}