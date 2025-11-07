using ExpenseTracker.Context;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Manager
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ExpenseContext _context;
        public CategoryManager(ExpenseContext context)
        {
            _context = context;
        }
        public void CreateCategory(Category category)
        {
            if(category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }
            if(_context.Categories.Any(k => k.CategoryName == category.CategoryName)) {
                throw new InvalidOperationException("A category with the same name already exists");
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Category ID must be greater than zero");
            }
            if(!_context.Categories.Any(c => c.CategoryId == id))
            {
                throw new KeyNotFoundException("Category not found");
            }
            _context.Categories.Remove(_context.Categories.First(c => c.CategoryId == id));
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            if(!_context.Categories.Any())
            {
                throw new InvalidOperationException("No categories found");
            }
            return await _context.Categories.ToHashSetAsync();
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Category ID must be greater than zero");
            }
            var category =  await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if(category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            return category;

        }

        public async Task<Category> GetCategoryByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Category name cannot be null or empty");
            }
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == name);
            if(category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            return category;
        }

        public void UpdateCategory(Guid id, Category category)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Category ID must be greater than zero");
            }
            if(category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null");
            }
            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if(existingCategory == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
