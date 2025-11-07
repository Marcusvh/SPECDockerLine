using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ExpenseTracker.Context
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserTransaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.EmailConfirmed).IsRequired().HasDefaultValue(false);

            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.CategoryName).IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.Description).IsRequired(false);
            modelBuilder.Entity<Category>().Property(c => c.CategoryType).IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.CategoryType).HasConversion<string>();

            modelBuilder.Entity<UserTransaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<UserTransaction>().Property(t => t.TransactionId);
            modelBuilder.Entity<UserTransaction>().Property(t => t.UserId).IsRequired();
            modelBuilder.Entity<UserTransaction>().Property(t => t.CategoryId).IsRequired();
            modelBuilder.Entity<UserTransaction>().Property(t => t.CreatedDate).IsRequired().HasPrecision(0);

            //modelBuilder.Entity<UserTransaction>()
            //    .HasOne(t => t.User)          // ← reference the navigation property
            //    .WithMany()                    // optional: add collection in User if you want
            //    .HasForeignKey(t => t.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<UserTransaction>()
            //    .HasOne(t => t.Category)      // ← reference the navigation property
            //    .WithMany()                    // optional: add collection in Category if you want
            //    .HasForeignKey(t => t.CategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
