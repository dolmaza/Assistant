using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configurations
{
    public class MeetingStatusConfiguration : EntityTypeConfiguration<MeetingStatus>
    {
        public MeetingStatusConfiguration()
        {
            ToTable("MeetingStatuses");

            HasKey(m => m.Id);

            Property(m => m.Caption)
                .IsRequired()
                .HasColumnType("nvarchar");
        }
    }
}
