using Bogus;
using ExpenseTracker.Context;
using ExpenseTracker.Models;

namespace ExpenseTracker.Seeding
{
    public static class DbSeeder
    {
        public static void Seed(ExpenseContext context)
        {
            try
            {
                if (context.Users.Any() || context.Categories.Any() || context.Transactions.Any())
                return; // Already seeded
            }
            catch (Exception)
            {
                // Handle the case where the database might not be created yet
                // or any other exception that occurs while checking for existing data.
                return;
            }
            

            // --- Seed Users ---
            var userFaker = new Faker<User>()
                .RuleFor(u => u.UserId, _ => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Password, f => f.Internet.Password(8))
                .RuleFor(u => u.EmailConfirmed, f => f.Random.Bool());

            var users = userFaker.Generate(10);
            context.Users.AddRange(users);

            // --- Seed Categories ---
            var categoryList = new List<Category>
        {
            new() { CategoryName = "Salary", Description = "Monthly income from job", CategoryType = CategoryType.Income },
            new() { CategoryName = "Freelance", Description = "Side job or contract work", CategoryType = CategoryType.Income },
            new() { CategoryName = "Investments", Description = "Stocks, crypto, dividends", CategoryType = CategoryType.Income },
            new() { CategoryName = "Food & Dining", Description = "Meals, groceries, restaurants", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Rent", Description = "House or apartment rent", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Utilities", Description = "Electricity, water, internet", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Transportation", Description = "Fuel, public transport, parking", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Health", Description = "Medical bills, fitness", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Entertainment", Description = "Movies, subscriptions, games", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Shopping", Description = "Clothing, electronics, etc.", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Travel", Description = "Trips, vacations, tickets", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Insurance", Description = "Health, car, or home insurance", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Gifts", Description = "Gifts or donations", CategoryType = CategoryType.Expense },
            new() { CategoryName = "Miscellaneous", Description = "Other small expenses", CategoryType = CategoryType.Expense }
        };
            context.Categories.AddRange(categoryList);
            context.SaveChanges();

            // --- Seed 1,000 UserTransactions ---
            var rand = new Random();
            var incomeCategories = categoryList.Where(c => c.CategoryType == CategoryType.Income).ToList();
            var expenseCategories = categoryList.Where(c => c.CategoryType == CategoryType.Expense).ToList();

            var transactionFaker = new Faker<UserTransaction>()
                .RuleFor(t => t.TransactionId, _ => Guid.NewGuid())
                .RuleFor(t => t.UserId, f => f.PickRandom(users).UserId)
                .RuleFor(t => t.CategoryId, f =>
                    f.Random.Bool(0.25f) // ~25% income, 75% expense
                    ? f.PickRandom(incomeCategories).CategoryId
                    : f.PickRandom(expenseCategories).CategoryId)
                .RuleFor(t => t.CreatedDate, f => f.Date.Between(DateTime.UtcNow.AddMonths(-12), DateTime.UtcNow))
                .RuleFor(t => t.Amount, (f, t) =>
                {
                    var cat = categoryList.First(c => c.CategoryId == t.CategoryId);
                    return cat.CategoryType == CategoryType.Income
                        ? f.Random.Decimal(1000, 5000)
                        : f.Random.Decimal(5, 300);
                })
                .RuleFor(t => t.Note, f => f.Lorem.Sentence(5));

            var transactions = transactionFaker.Generate(1000);
            context.Transactions.AddRange(transactions);

            context.SaveChanges();
        }

    }
}
