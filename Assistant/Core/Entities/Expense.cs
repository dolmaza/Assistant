using System;

namespace Core.Entities
{
    public class Expense
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string ExpenseCaption { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? CreateTime { get; set; }

        public User User { get; set; }

        public Expense()
        {
            CreateTime = DateTime.Now;
        }

    }
}
