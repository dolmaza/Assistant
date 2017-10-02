using Core.DB.Configurations;
using Core.Entities;
using System.Data.Entity;

namespace Core.DB
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new ExpenseConfiguration());
            modelBuilder.Configurations.Add(new MeetingConfiguration());
            modelBuilder.Configurations.Add(new MeetingStatusConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingStatus> MeetingStatuses { get; set; }
    }
}
