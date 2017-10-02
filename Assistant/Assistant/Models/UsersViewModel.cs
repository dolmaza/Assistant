using System.Collections.Generic;

namespace Assistant.Models
{
    public class UsersViewModel
    {
        #region Properties
        public string CreateUserUrl { get; set; }
        public List<UserGridItem> UserGridItems { get; set; }
        #endregion

        #region Sub Classes
        public class UserGridItem
        {
            #region Properties
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public bool HasAvatar => !string.IsNullOrWhiteSpace(Avatar);
            public string Avatar { get; set; }
            public string BirthDate { get; set; }
            public string Budget { get; set; }

            public string UpdateUrl { get; set; }
            public string DeleteUrl { get; set; }
            #endregion
        }
        #endregion
    }

    public class UsersCreateViewModel
    {
        public string UsersUrl { get; set; }
        public string SaveUrl { get; set; }
    }

    public class UsersUpdateViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool HasAvatar => !string.IsNullOrWhiteSpace(Avatar);
        public string Avatar { get; set; }
        public string BirthDate { get; set; }
        public string Budget { get; set; }

        public string UsersUrl { get; set; }
        public string SaveUrl { get; set; }
    }
}