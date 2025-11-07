using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models
{
    public class UserTransaction
    {
        public Guid? TransactionId { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }

        public User? User { get; set; }
        public Category? Category { get; set; }

        public UserTransaction()
        {
            TransactionId = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            Math.Round(Amount, 2);
        }
    }
}
