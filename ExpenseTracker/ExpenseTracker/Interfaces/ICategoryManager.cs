using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces
{
    public interface ICategoryManager
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(Guid id);
        Task<Category> GetCategoryByName(string name);
        void CreateCategory(Category category);
        void UpdateCategory(Guid id, Category category);
        void DeleteCategory(Guid id);
    }
}
