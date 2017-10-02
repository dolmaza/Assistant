using System;

namespace Assistant.SubmitModels
{
    public class UserUpdateSubmitModel
    {
        public int userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarBase64 { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Budget { get; set; }
    }
}