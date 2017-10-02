using System;

namespace Assistant.SubmitModels
{
    public class UserCreateSubmitModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarBase64 { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Budget { get; set; }
    }
}