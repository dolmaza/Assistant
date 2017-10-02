using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Budget { get; set; }
        public DateTime? CreateTime { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Meeting> Meetings { get; set; }

        public User()
        {
            Expenses = new List<Expense>();
            Meetings = new List<Meeting>();

            CreateTime = DateTime.Now;
        }
    }
}
