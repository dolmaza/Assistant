using Service.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Service
{
    [ServiceContract]
    public interface IMeetingService
    {
        [OperationContract]
        IEnumerable<MeetingServiceModel> GetAllMeetings(int? userId, int? meetingStatusCode, DateTime? startDate, DateTime? endDate);

        [OperationContract]
        IEnumerable<MeetingStatusServiceModel> GetMeetingStatuses();

        [OperationContract]
        int? CreateMeeting(MeetingServiceModel model);

        [OperationContract]
        void UpdateMeeting(MeetingServiceModel model);

        [OperationContract]
        void DeleteMeeting(int? Id);
    }
}
