using System;

namespace Assistant.SubmitModels
{
    public class MeetingUpdateSubmitModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? MeetingStatusId { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public DateTime? MeetingTime { get; set; }
    }
}