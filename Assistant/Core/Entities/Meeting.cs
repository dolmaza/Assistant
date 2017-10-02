using System;

namespace Core.Entities
{
    public class Meeting
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? MeetingStatusId { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public DateTime? MeetingTime { get; set; }
        public DateTime? CreateTime { get; set; }

        public User User { get; set; }
        public MeetingStatus MeetingStatus { get; set; }

        public Meeting()
        {
            CreateTime = DateTime.Now;
        }
    }
}
