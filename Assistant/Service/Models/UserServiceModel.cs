using System;
using System.Runtime.Serialization;

namespace Service.Models
{
    [DataContract]
    public class UserServiceModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public decimal? Budget { get; set; }

        [DataMember]
        public DateTime? CreateTime { get; set; }
    }
}