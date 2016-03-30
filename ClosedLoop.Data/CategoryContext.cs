using System.Data.Entity;
using ClosedLoop.Models;

namespace ClosedLoop.Data
{
    public class CategoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        public CategoryContext()
        {
        }

        public CategoryContext(string connectionName) : base(connectionName)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //one-to-many 
            modelBuilder.Entity<Category>()
                        .HasOptional(c => c.ParentCategory)
                        .WithMany(s => s.SubCategories)
                        .HasForeignKey(s => s.ParentCategoryId);

        }
    }
}
