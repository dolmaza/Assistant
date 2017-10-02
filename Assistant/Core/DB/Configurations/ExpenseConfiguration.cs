using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class ExpenseConfiguration : EntityTypeConfiguration<Expense>
    {
        public ExpenseConfiguration()
        {
            ToTable("Expenses");

            HasKey(e => e.Id);

            Property(e => e.ExpenseCaption)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(500);

            Property(e => e.Amount)
                .IsRequired()
                .HasColumnType("money");

            HasRequired(e => e.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}
