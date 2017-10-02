using System;
using System.Runtime.Serialization;

namespace Service.Models
{
    [DataContract]
    public class ExpenseServiceModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int? UserId { get; set; }

        [DataMember]
        public string ExpenseCaption { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public DateTime? CreateTime { get; set; }
    }
}