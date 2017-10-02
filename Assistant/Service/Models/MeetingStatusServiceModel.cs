using System;
using System.Runtime.Serialization;

namespace Service.Models
{
    [DataContract]
    public class MeetingStatusServiceModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public int? Code { get; set; }

        [DataMember]
        public DateTime? CreateTime { get; set; }
    }
}