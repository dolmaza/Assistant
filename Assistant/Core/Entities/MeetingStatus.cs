using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class MeetingStatus
    {
        public int? Id { get; set; }
        public string Caption { get; set; }
        public int? Code { get; set; }
        public DateTime? CrateTime { get; set; }

        public ICollection<Meeting> Meetings { get; set; }

        public MeetingStatus()
        {
            Meetings = new List<Meeting>();

            CrateTime = DateTime.Now;
        }
    }
}
