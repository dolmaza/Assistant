using Core.Entities;
using Core.Repository;
using Service.Models;
using Service.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class MeetingService : BaseService, IMeetingService
    {
        private IRepository<Meeting> _meetingRepository;
        private IRepository<MeetingStatus> _meetingStatusRepository;

        public MeetingService()
        {
            _meetingRepository = GetRepository<Meeting>();
            _meetingStatusRepository = GetRepository<MeetingStatus>();
        }

        public int? CreateMeeting(MeetingServiceModel model)
        {
            var meeting = new Meeting
            {
                UserId = model.UserId,
                MeetingStatusId = model.MeetingStatusId,
                PersonFirstName = model.PersonFirstName,
                PersonLastName = model.PersonLastName,
                MeetingTime = model.MeetingTime
            };

            _meetingRepository.Add(meeting);
            _meetingRepository.Complete();

            if (_meetingRepository.IsError)
            {
                throw new Exception(Resources.TextAbort);
            }

            return meeting.Id;
        }

        public void DeleteMeeting(int? Id)
        {
            var meeting = _meetingRepository.GetByID(Id);

            if (meeting == null)
            {
                throw new Exception(Resources.ValidationMeetingNotFound);
            }

            _meetingRepository.Remove(meeting);
            _meetingRepository.Complete();

            if (_meetingRepository.IsError)
            {
                throw new Exception(Resources.TextAbort);
            }
        }

        public IEnumerable<MeetingServiceModel> GetAllMeetings(int? userId, int? meetingStatusId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _meetingRepository.Get(filter: m => m.UserId == userId
                                                                        && (meetingStatusId == null || meetingStatusId == m.MeetingStatus.Id)
                                                                        && ((startDate.Value == null && endDate.Value == null)
                                                                        || (startDate.Value != null && endDate.Value == null && m.MeetingTime.Value >= startDate.Value)
                                                                        || (startDate.Value == null && endDate.Value != null && m.MeetingTime.Value <= endDate.Value)
                                                                        || (startDate.Value != null && endDate.Value != null && m.MeetingTime.Value >= startDate.Value && m.MeetingTime.Value <= endDate.Value))
                                                ,
                                            orderBy: ob => ob.OrderByDescending(m => m.CreateTime),
                                            includes: m => m.MeetingStatus).Select(m => new MeetingServiceModel
                                            {
                                                Id = m.Id,
                                                UserId = m.UserId,
                                                MeetingStatusId = m.MeetingStatusId,
                                                PersonFirstName = m.PersonFirstName,
                                                PersonLastName = m.PersonLastName,
                                                MeetingTime = m.MeetingTime,
                                                MeetingStatusCaption = m.MeetingStatus.Caption,
                                                MeetingStatusCode = m.MeetingStatus.Code,
                                                CreateTime = m.CreateTime
                                            }).ToList();
        }

        public IEnumerable<MeetingStatusServiceModel> GetMeetingStatuses()
        {
            return _meetingStatusRepository.GetAll().Select(m => new MeetingStatusServiceModel
            {
                Id = m.Id,
                Caption = m.Caption,
                Code = m.Code,
                CreateTime = m.CrateTime
            }).ToList();
        }

        public void UpdateMeeting(MeetingServiceModel model)
        {
            var meeting = _meetingRepository.GetByID(model.Id);

            if (meeting == null)
            {
                throw new Exception(Resources.ValidationMeetingNotFound);
            }

            meeting.MeetingStatusId = model.MeetingStatusId;
            meeting.PersonFirstName = model.PersonFirstName;
            meeting.PersonLastName = model.PersonLastName;
            meeting.MeetingTime = model.MeetingTime;

            _meetingRepository.Update(meeting);
            _meetingRepository.Complete();

            if (_meetingRepository.IsError)
            {
                throw new Exception(Resources.TextAbort);
            }
        }
    }
}
