using Assistant.Models;
using Assistant.SubmitModels;
using Service;
using Service.Models;
using Service.Utiliteis;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assistant.Reusables.Helpers
{
    public class UsersHelper
    {
        private UserService _userService;
        private ExpenseService _expenseService;
        private MeetingService _meetingService;

        public UsersHelper()
        {
            _userService = new UserService();
            _expenseService = new ExpenseService();
            _meetingService = new MeetingService();
        }

        public UsersViewModel GetUsersViewModel(UrlHelper url)
        {
            return new UsersViewModel
            {
                CreateUserUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS_ADD),
                UserGridItems = _userService.GetAllUsers().Select(u => new UsersViewModel.UserGridItem
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Avatar = u.Avatar,
                    BirthDate = u.BirthDate?.ToString(Constants.Formats.Date),
                    Budget = $"{u.Budget:#.####}",

                    UpdateUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS_UPDATE, new { userId = u.Id }),
                    DeleteUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS_DELETE, new { userId = u.Id })
                }).ToList()
            };
        }

        public UsersCreateViewModel GetUsersCreateViewModel(UrlHelper url)
        {
            return new UsersCreateViewModel
            {
                UsersUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS),
                SaveUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS_ADD)
            };
        }

        public UsersUpdateViewModel GetUsersUpdateViewModel(UrlHelper url, int? Id)
        {
            var user = _userService.GetUserById(Id);

            if (user == null)
            {
                return null;
            }

            return new UsersUpdateViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                BirthDate = user.BirthDate?.ToString(Constants.Formats.Date),
                Budget = $"{user.Budget:#.####}",
                UsersUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS),
                SaveUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS_UPDATE)
            };
        }



        public AjaxResponse CreateUser(UserCreateSubmitModel submitModel)
        {
            var ajaxResponse = new AjaxResponse();
            try
            {
                _userService.CreateUser(new UserServiceModel
                {
                    Avatar = submitModel.AvatarBase64,
                    FirstName = submitModel.FirstName,
                    LastName = submitModel.LastName,
                    BirthDate = submitModel.BirthDate,
                    Budget = submitModel.Budget
                });

                ajaxResponse.IsSuccess = true;

            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }
            return ajaxResponse;
        }

        public AjaxResponse UpdateUser(UserUpdateSubmitModel submitModel)
        {
            var ajaxResponse = new AjaxResponse();
            try
            {
                _userService.UpdateUser(new UserServiceModel
                {
                    Id = submitModel.userId,
                    Avatar = submitModel.AvatarBase64,
                    FirstName = submitModel.FirstName,
                    LastName = submitModel.LastName,
                    BirthDate = submitModel.BirthDate,
                    Budget = submitModel.Budget
                });

                ajaxResponse.IsSuccess = true;

            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }
            return ajaxResponse;
        }

        public AjaxResponse DeleteUser(int? Id)
        {
            var ajaxResponse = new AjaxResponse();
            try
            {
                _userService.DeleteUser(Id);
                ajaxResponse.IsSuccess = true;

            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }
            return ajaxResponse;
        }


        #region User Expenses
        public ExpensesViewModel GetExpensesViewModel(UrlHelper url, int? userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var user = _userService.GetUserById(userId);
            var expenses = _expenseService.GetAllExpenses(userId, startDate, endDate).ToList();


            if (user == null || expenses == null)
            {
                return null;
            }

            var expensesSum = expenses.Sum(e => e.Amount);
            expensesSum = expensesSum ?? 0;
            var moneyLeft = user.Budget - expensesSum;
            moneyLeft = moneyLeft ?? 0;

            return new ExpensesViewModel
            {
                UserFullName = $"{user.FirstName} {user.LastName}",
                UserBudget = $"{user.Budget:#.####}",
                ExpensesSum = expensesSum == 0 ? "0" : $"{expensesSum:#.####}",
                MoneyLeft = moneyLeft == 0 ? "0" : $"{moneyLeft:#.####}",

                ExpenseItems = expenses.Select(e => new ExpensesViewModel.ExpenseItem
                {
                    Id = e.Id,
                    ExpenseCaption = e.ExpenseCaption,
                    Amount = $"{e.Amount:#.####}",

                    UpdateUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_EXPENSES_UPDATE, new { userId = e.UserId, expenseId = e.Id }),
                    DeleteUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_EXPENSES_DELETE, new { userId = e.UserId, expenseId = e.Id })
                }).ToList(),

                UsersUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS),
                CreateExpenseUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_EXPENSES_CREATE, new { userId = user.Id }),
                FilterExpenseUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_EXPENSES_FILTERED, new { userId = user.Id }),
                ExportToExcelUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_EXPENSES_EXPORT_TO_EXCEL, new { userId = user.Id })

            };
        }

        public AjaxResponse CreateUserExpense(int? userId, ExpenseCreateSubmitModel submitModel)
        {
            var ajaxResponse = new AjaxResponse();

            try
            {
                var newExpenseId = _expenseService.CreateExpense(new ExpenseServiceModel
                {
                    UserId = userId,
                    ExpenseCaption = submitModel.ExpenseCaption,
                    Amount = submitModel.Amount
                });

                ajaxResponse.IsSuccess = true;
                ajaxResponse.Data = new
                {
                    ExpenseId = newExpenseId
                };
            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }

            return ajaxResponse;
        }

        public AjaxResponse UpdateUserExpense(ExpenseUpdateSubmitModel submitModel)
        {
            var ajaxResponse = new AjaxResponse();

            try
            {
                _expenseService.UpdateExpense(new ExpenseServiceModel
                {
                    Id = submitModel.ExpenseId,
                    ExpenseCaption = submitModel.ExpenseCaption,
                    Amount = submitModel.Amount
                });

                ajaxResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }

            return ajaxResponse;
        }

        public AjaxResponse DeleteUserExpense(int? expenseId)
        {
            var ajaxResponse = new AjaxResponse();

            try
            {
                _expenseService.DeleteExpense(expenseId);

                ajaxResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }

            return ajaxResponse;
        }

        public AjaxResponse GetFilteredExpenses(UrlHelper url, int? userId, DateTime? startDate, DateTime? endDate)
        {
            var ajaxResponse = new AjaxResponse();
            var model = GetExpensesViewModel(url, userId, startDate, endDate);
            if (model != null)
            {
                ajaxResponse.IsSuccess = true;
                ajaxResponse.Data = new
                {
                    filteredExpenses = model
                };
            }

            return ajaxResponse;
        }

        public void InitExpensesDataForExcel(Controller controller, int? userId, DateTime? startDate, DateTime? endDate)
        {
            var expenses = _expenseService.GetAllExpenses(userId, startDate, endDate).ToList();
            if (expenses != null)
            {
                var data = new DataTable("expneses");

                data.Columns.Add("Expense Caption", typeof(string));
                data.Columns.Add("Amount (₾)", typeof(string));

                foreach (var expense in expenses)
                {
                    data.Rows.Add
                        (
                            expense.ExpenseCaption,
                            $"{expense.Amount:#.####}"
                        );

                }

                ExportToExcel(data, controller, "Expenses");
            }
        }
        #endregion


        #region User Meetings
        public MeetingsViewModel GetMeetingsViewModel(UrlHelper url, int? userId, int? meetingStatusId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var user = _userService.GetUserById(userId);
            var meetings = _meetingService.GetAllMeetings(userId, meetingStatusId, startDate, endDate);

            if (user == null || meetings == null)
            {
                return null;
            }

            return new MeetingsViewModel
            {
                UserFullName = $"{user.FirstName} {user.LastName}",
                UsersUrl = url.RouteUrl(ControllerActionRouteNames.Users.USERS),

                CreateMeetingUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_MEETINGS_CREATE, new { userId = userId }),
                FilterMeetingUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_MEETINGS_FILTERED, new { userId = userId }),
                ExportToExcelUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_MEETINGS_EXPORT_TO_EXCEL, new { userId = userId }),

                MeetingItems = meetings.Select(m => new MeetingsViewModel.MeetingItem
                {
                    Id = m.Id,
                    MeetingStatusId = m.MeetingStatusId,
                    PersonFirstName = m.PersonFirstName,
                    PersonLastName = m.PersonLastName,
                    MeetingTime = m.MeetingTime?.ToString(Constants.Formats.DateTime),
                    MeetingStatusCaption = m.MeetingStatusCaption,
                    MeetingStatusClassName = GetMeetingStatusClassName(m.MeetingStatusCode),

                    UpdateUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_MEETINGS_UPDATE, new { userId = userId, meetingId = m.Id }),
                    DeleteUrl = url.RouteUrl(ControllerActionRouteNames.Users.USER_MEETINGS_DELETE, new { userId = userId, meetingId = m.Id })
                }).ToList(),

                MeetingStatuses = _meetingService.GetMeetingStatuses().Select(m => new SimpleKeyValue<int?, string>
                {
                    Key = m.Id,
                    Value = m.Caption,
                    IsSelected = m.Id == meetingStatusId
                }).ToList()
            };
        }

        public AjaxResponse CreateUserMeeting(int? userId, MeetingCreateSubmitModel submitModel)
        {
            var ajaxResponse = new AjaxResponse();

            try
            {
                _meetingService.CreateMeeting(new MeetingServiceModel
                {
                    MeetingStatusId = submitModel.MeetingStatusId,
                    UserId = userId,
                    PersonFirstName = submitModel.PersonFirstName,
                    PersonLastName = submitModel.PersonLastName,
                    MeetingTime = submitModel.MeetingTime
                });

                ajaxResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }

            return ajaxResponse;
        }

        public AjaxResponse UpdateUserMeeting(int? userId, int? meetingId, MeetingUpdateSubmitModel submitModel)
        {
            var ajaxResponse = new AjaxResponse();

            try
            {
                _meetingService.UpdateMeeting(new MeetingServiceModel
                {
                    Id = meetingId,
                    MeetingStatusId = submitModel.MeetingStatusId,
                    PersonFirstName = submitModel.PersonFirstName,
                    PersonLastName = submitModel.PersonLastName,
                    MeetingTime = submitModel.MeetingTime
                });

                ajaxResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }

            return ajaxResponse;
        }

        public AjaxResponse GetFilteredMeetings(UrlHelper url, int? userId, int? meetingStatusId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var ajaxResponse = new AjaxResponse();
            var model = GetMeetingsViewModel(url, userId, meetingStatusId, startDate, endDate);
            if (model != null)
            {
                ajaxResponse.IsSuccess = true;
                ajaxResponse.Data = new
                {
                    filteredMeetings = model
                };
            }
            return ajaxResponse;
        }

        public AjaxResponse DeleteUserMeeting(int? Id)
        {
            var ajaxResponse = new AjaxResponse();

            try
            {
                _meetingService.DeleteMeeting(Id);
                ajaxResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                ajaxResponse.Data = new
                {
                    Message = ex.Message
                };
            }

            return ajaxResponse;
        }

        public void InitMeetingsDataForExcel(Controller controller, int? userId, int? meetingStatusId, DateTime? startDate, DateTime? endDate)
        {
            var meetings = _meetingService.GetAllMeetings(userId, meetingStatusId, startDate, endDate).ToList();
            if (meetings != null)
            {
                var data = new DataTable("meetings");

                data.Columns.Add("First name", typeof(string));
                data.Columns.Add("Last name", typeof(string));
                data.Columns.Add("Meeting time", typeof(string));
                data.Columns.Add("Status", typeof(string));

                foreach (var meeting in meetings)
                {
                    data.Rows.Add
                        (
                            meeting.PersonFirstName,
                            meeting.PersonLastName,
                            meeting.MeetingTime?.ToString(Constants.Formats.DateTime),
                            meeting.MeetingStatusCaption
                        );

                }

                ExportToExcel(data, controller, "Meetings");
            }
        }
        #endregion

        public void ExportToExcel(DataTable data, Controller controller, string fileName)
        {
            var gridview = new GridView
            {
                DataSource = data
            };
            gridview.DataBind();

            controller.Response.ClearContent();
            controller.Response.Buffer = true;

            controller.Response.AddHeader("content-disposition", $"attachment; filename = {fileName}.xls");
            controller.Response.ContentType = "application/ms-excel";
            controller.Response.Charset = "";

            using (var sw = new StringWriter())
            {
                using (var htw = new HtmlTextWriter(sw))
                {
                    gridview.RenderControl(htw);

                    controller.Response.Output.Write(sw.ToString());
                    controller.Response.Flush();
                    controller.Response.End();
                }
            }
        }

        public string GetMeetingStatusClassName(int? meetingStatusCode)
        {
            string meetingStatusClassName = null;

            switch (meetingStatusCode)
            {
                case MeetingStatuses.Met:
                    meetingStatusClassName = "success";
                    break;
                case MeetingStatuses.NotMet:
                    meetingStatusClassName = "danger";
                    break;
                default:
                    break;
            }

            return meetingStatusClassName;
        }
    }
}