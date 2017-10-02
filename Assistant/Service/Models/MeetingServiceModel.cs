using System;
using System.Runtime.Serialization;

namespace Service.Models
{
    [DataContract]
    public class MeetingServiceModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public int? MeetingStatusId { get; set; }

        [DataMember]
        public string MeetingStatusCaption { get; set; }

        [DataMember]
        public int? MeetingStatusCode { get; set; }

        [DataMember]
        public string PersonFirstName { get; set; }

        [DataMember]
        public string PersonLastName { get; set; }

        [DataMember]
        public DateTime? MeetingTime { get; set; }

        [DataMember]
        public DateTime? CreateTime { get; set; }
    }
}