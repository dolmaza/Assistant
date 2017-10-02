using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasKey(u => u.Id);

            Property(u => u.FirstName)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(200);

            Property(u => u.LastName)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(200);

            Property(u => u.Budget)
                .HasColumnType("money");


        }
    }
}
