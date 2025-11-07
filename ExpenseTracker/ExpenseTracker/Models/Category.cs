using System.Text.Json.Serialization;

namespace ExpenseTracker.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CategoryType
    {
        Income,
        Expense
    }
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public CategoryType CategoryType { get; set; }
        
        public Category()
        {
            CategoryId = Guid.NewGuid();
        }
    }
}
