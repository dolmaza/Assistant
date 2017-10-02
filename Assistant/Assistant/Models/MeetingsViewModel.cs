using Assistant.Reusables;
using System.Collections.Generic;

namespace Assistant.Models
{
    public class MeetingsViewModel
    {
        #region Properties
        public List<MeetingItem> MeetingItems { get; set; }
        public List<SimpleKeyValue<int?, string>> MeetingStatuses { get; set; }

        public string UserFullName { get; set; }

        public string UsersUrl { get; set; }
        public string CreateMeetingUrl { get; set; }
        public string FilterMeetingUrl { get; set; }
        public string ExportToExcelUrl { get; set; }
        #endregion

        #region Sub Classes
        public class MeetingItem
        {
            #region Properties
            public int? Id { get; set; }
            public int? UserId { get; set; }
            public int? MeetingStatusId { get; set; }
            public string MeetingStatusCaption { get; set; }
            public string MeetingStatusClassName { get; set; }
            public string PersonFirstName { get; set; }
            public string PersonLastName { get; set; }
            public string MeetingTime { get; set; }

            public string UpdateUrl { get; set; }
            public string DeleteUrl { get; set; }
            #endregion
        }
        #endregion
    }
}