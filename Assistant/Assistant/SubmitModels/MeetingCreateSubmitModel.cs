using System;

namespace Assistant.SubmitModels
{
    public class MeetingCreateSubmitModel
    {
        public int? UserId { get; set; }
        public int? MeetingStatusId { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public DateTime? MeetingTime { get; set; }
    }
}