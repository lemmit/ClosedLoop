using ClosedLoop.Models;
using System;

namespace ClosedLoop.Repositories
{
    public interface ICategoryRepository : IDisposable
    {
        Category GetRootCategory();
        Category GetCategory(string category);
        void Clear();
        void Add(Category category);
        void Save();
    }
}