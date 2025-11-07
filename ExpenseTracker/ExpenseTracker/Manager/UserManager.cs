using ExpenseTracker.Context;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace ExpenseTracker.Manager
{
    public class UserManager : IUserManager
    {
        private readonly ExpenseContext _context;
        public UserManager(ExpenseContext context)
        {
            _context = context;
        }
        public void CreateUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            if(_context.Users.Any(u => u.Email == user.Email))
            {
                throw new InvalidOperationException("A user with the same email already exists");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            if(Guid.Empty == id)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "User ID must be greater than zero");
            }
            if(!_context.Users.Any(u => u.UserId == id))
            {
                throw new KeyNotFoundException("User not found");
            }

            _context.Users.Remove(_context.Users.First(u => u.UserId == id));
            _context.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToHashSetAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            if(Guid.Empty == id)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "User ID must be greater than zero");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if(user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }

        public void UpdateUser(Guid id, User user)
        {
            if(Guid.Empty == id)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "User ID must be greater than zero");
            }
            if(user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }
            var existingUser = _context.Users.FirstOrDefault(u => u.UserId == id);
            if(existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            _context.Users.Update(user);
        }
    }
}
