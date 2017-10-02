using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class MeetingConfiguration : EntityTypeConfiguration<Meeting>
    {
        public MeetingConfiguration()
        {
            ToTable("Meetings");

            HasKey(m => m.Id);

            Property(m => m.PersonFirstName)
                .IsRequired()
                .HasColumnType("nvarchar");


            Property(m => m.PersonLastName)
                .IsRequired()
                .HasColumnType("nvarchar");

            HasRequired(m => m.MeetingStatus)
                .WithMany(m => m.Meetings)
                .HasForeignKey(m => m.MeetingStatusId)
                .WillCascadeOnDelete(false);

            HasRequired(m => m.User)
                .WithMany(u => u.Meetings)
                .HasForeignKey(m => m.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}
