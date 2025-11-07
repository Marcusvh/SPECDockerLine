using ExpenseTracker.Models;

namespace ExpenseTracker.Interfaces
{
    public interface IUserManager
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        void CreateUser(User user);
        void UpdateUser(Guid id, User user);
        void DeleteUser(Guid id);
    }
}
