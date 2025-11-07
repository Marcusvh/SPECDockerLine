using ExpenseTracker.Context;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace ExpenseTracker.Manager
{
    public class TransactionManager : ITransactionManager
    {
        private readonly ExpenseContext _context;
        public TransactionManager(ExpenseContext context)
        {
            _context = context;
        }
        public void CreateTransaction(UserTransaction UserTransaction)
        {
            if(UserTransaction == null)
            {
                throw new ArgumentNullException(nameof(UserTransaction), "UserTransaction cannot be null");
            }
            if(_context.Transactions.Any(t => t.TransactionId == UserTransaction.TransactionId))
            {
                throw new InvalidOperationException("A UserTransaction with the same ID already exists");
            }
            _context.Transactions.Add(UserTransaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Transaction ID must be greater than zero");
            }
            if(!_context.Transactions.Any(t => t.TransactionId == id))
            {
                throw new KeyNotFoundException("Transaction not found");
            }
            _context.Transactions.Remove(_context.Transactions.First(t => t.TransactionId == id));
            _context.SaveChanges();
        }

        public async Task<IEnumerable<UserTransaction>> GetAllTransactions()
        {
            if(!_context.Transactions.Any())
            {
                throw new InvalidOperationException("No transactions found");
            }
          
            return await _context.Transactions.Include(t => t.Category).Include(t => t.User).ToHashSetAsync();
        }

        public async Task<UserTransaction> GetTransactionById(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Transaction ID must be greater than zero");
            }
            var transaction = await _context.Transactions.Include(o => o.User).Include(o => o.Category).FirstOrDefaultAsync(t => t.TransactionId == id);
            if(transaction == null)
            {
                throw new KeyNotFoundException("Transaction not found");
            }
            return transaction;
        }

        public async Task<IEnumerable<UserTransaction>> GetTransactionsByCategoryId(Guid categoryId)
        {
            if(categoryId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId), "Category ID must be greater than zero");
            }
            var transactions = await _context.Transactions.Where(t => t.CategoryId == categoryId).Include(o => o.User).Include(o => o.Category).ToListAsync();
            if(transactions == null || !transactions.Any())
            {
                throw new KeyNotFoundException("No transactions found for the given category ID");
            }
            return transactions;
        }

        public async Task<IEnumerable<UserTransaction>> GetTransactionsByName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            }
            Guid categoryIdByName = _context.Categories.FirstOrDefault(n => n.CategoryName == name).CategoryId;
            var transactions = await _context.Transactions.Where(t => t.CategoryId == categoryIdByName).Include(o => o.User).Include(o => o.Category).ToHashSetAsync();
            return transactions;
        }

        public async Task<IEnumerable<UserTransaction>> GetTransactionsByUserId(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be greater than zero");
            }
            var transactions =  await _context.Transactions.Where(t => t.UserId == userId).Include(o => o.User).Include(o => o.Category).ToListAsync();
            if(transactions == null || !transactions.Any())
            {
                throw new KeyNotFoundException("No transactions found for the given user ID");
            }
            return transactions;
        }

        public void UpdateTransaction(Guid id, UserTransaction UserTransaction)
        {
            // hmm maybe, maybe not. that is the question
            throw new NotImplementedException();
        }
    }
}
