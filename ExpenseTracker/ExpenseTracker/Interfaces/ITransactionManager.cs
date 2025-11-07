using ExpenseTracker.Models;
using System.Transactions;

namespace ExpenseTracker.Interfaces
{
    public interface ITransactionManager
    {
        Task<IEnumerable<UserTransaction>> GetAllTransactions();
        Task<UserTransaction> GetTransactionById(Guid id);
        Task<IEnumerable<UserTransaction>> GetTransactionsByUserId(Guid userId);
        Task<IEnumerable<UserTransaction>> GetTransactionsByCategoryId(Guid categoryId);
        Task<IEnumerable<UserTransaction>> GetTransactionsByName(string name);
        void CreateTransaction(UserTransaction UserTransaction);
        void UpdateTransaction(Guid id, UserTransaction UserTransaction);
        void DeleteTransaction(Guid id);
    }
}
