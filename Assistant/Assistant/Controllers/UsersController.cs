using Assistant.Reusables;
using Assistant.Reusables.Filters;
using Assistant.Reusables.Helpers;
using Assistant.SubmitModels;
using System;
using System.Web.Mvc;

namespace Assistant.Controllers
{
    public class UsersController : BaseController
    {
        private UsersHelper _usersHelper;

        public UsersController()
        {
            _usersHelper = new UsersHelper();
        }

        [Route("users", Name = ControllerActionRouteNames.Users.USERS)]
        public ActionResult Users()
        {
            var model = _usersHelper.GetUsersViewModel(Url);
            return View(model);
        }

        [Route("users/create", Name = ControllerActionRouteNames.Users.USERS_ADD)]
        public ActionResult UsersCreate()
        {
            var model = _usersHelper.GetUsersCreateViewModel(Url);
            return View(model);
        }

        [HttpPost]
        [Route("users/create")]
        public ActionResult UsersCreate(UserCreateSubmitModel submitModel)
        {
            var ajaxResponse = _usersHelper.CreateUser(submitModel);
            return Json(ajaxResponse);
        }

        [Route("users/{userId}/delete", Name = ControllerActionRouteNames.Users.USERS_DELETE)]
        public ActionResult UsersDelete(int? userId)
        {
            _usersHelper.DeleteUser(userId);
            return RedirectToRoute(ControllerActionRouteNames.Users.USERS);
        }

    }

    [BeforeUserPageLoad(Order = 2)]
    [RoutePrefix("users/{userId}")]
    public class UserController : BaseController
    {
        private UsersHelper _usersHelper;

        public UserController()
        {
            _usersHelper = new UsersHelper();
        }

        [Route("update", Name = ControllerActionRouteNames.Users.USERS_UPDATE)]
        public ActionResult UsersUpdate(int? userId)
        {
            var model = _usersHelper.GetUsersUpdateViewModel(Url, userId);

            if (model == null)
            {
                return NotFound();
            }
            else
            {
                return View(model);
            }

        }

        [HttpPost]
        [Route("update")]
        public ActionResult UsersUpdate(UserUpdateSubmitModel submitModel)
        {
            var ajaxResponse = _usersHelper.UpdateUser(submitModel);

            return Json(ajaxResponse);
        }

        #region User Expenses
        [Route("expenses", Name = ControllerActionRouteNames.Users.USER_EXPENSES)]
        public ActionResult UserExpenses(int? userId)
        {
            var model = _usersHelper.GetExpensesViewModel(Url, userId);
            return View(model);
        }

        [Route("expenses/create", Name = ControllerActionRouteNames.Users.USER_EXPENSES_CREATE)]
        public ActionResult UserExpensesCreate(int? userId, ExpenseCreateSubmitModel submitModel)
        {
            var ajaxResponse = _usersHelper.CreateUserExpense(userId, submitModel);
            return Json(ajaxResponse);
        }

        [Route("expenses/{expenseId}/update", Name = ControllerActionRouteNames.Users.USER_EXPENSES_UPDATE)]
        public ActionResult UserExpensesUpdate(int? userId, ExpenseUpdateSubmitModel submitModel)
        {
            var ajaxResponse = _usersHelper.UpdateUserExpense(submitModel);
            return Json(ajaxResponse);
        }

        [Route("expenses/{expenseId}/delete", Name = ControllerActionRouteNames.Users.USER_EXPENSES_DELETE)]
        public ActionResult UserExpensesDelete(int? userId, int? expenseId)
        {
            _usersHelper.DeleteUserExpense(expenseId);
            return RedirectToRoute(ControllerActionRouteNames.Users.USER_EXPENSES);
        }

        [HttpPost]
        [Route("expenses/filtered", Name = ControllerActionRouteNames.Users.USER_EXPENSES_FILTERED)]
        public ActionResult GetFilteredExpenses(int? userId, DateTime? startDate, DateTime? endDate)
        {
            var ajaxResponse = _usersHelper.GetFilteredExpenses(Url, userId, startDate, endDate);
            return Json(ajaxResponse);
        }

        [HttpPost]
        [Route("expenses/export-to-excel", Name = ControllerActionRouteNames.Users.USER_EXPENSES_EXPORT_TO_EXCEL)]
        public ActionResult ExpensesExportToExcel(int? userId, DateTime? startDate, DateTime? endDate)
        {
            _usersHelper.InitExpensesDataForExcel(this, userId, startDate, endDate);
            return RedirectToRoute(ControllerActionRouteNames.Users.USER_EXPENSES, new { userId = userId });
        }
        #endregion


        #region User Meetings

        [Route("meetings", Name = ControllerActionRouteNames.Users.USER_MEETINGS)]
        public ActionResult UserMeetings(int? userId)
        {
            var model = _usersHelper.GetMeetingsViewModel(Url, userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Route("meetings/filtered", Name = ControllerActionRouteNames.Users.USER_MEETINGS_FILTERED)]
        public ActionResult GetFilteredMeetings(int? userId, int? meetingStatusId, DateTime? startDate, DateTime? endDate)
        {
            var ajaxResponse = _usersHelper.GetFilteredMeetings(Url, userId, meetingStatusId, startDate, endDate);
            return Json(ajaxResponse);
        }

        [HttpPost]
        [Route("meetings/create", Name = ControllerActionRouteNames.Users.USER_MEETINGS_CREATE)]
        public ActionResult UserMeetingsCreate(int? userId, MeetingCreateSubmitModel submitModel)
        {
            var ajaxResponse = _usersHelper.CreateUserMeeting(userId, submitModel);
            return Json(ajaxResponse);
        }

        [HttpPost]
        [Route("meetings/{meetingId}/update", Name = ControllerActionRouteNames.Users.USER_MEETINGS_UPDATE)]
        public ActionResult UserMeetingsUpdate(int? userId, int? meetingId, MeetingUpdateSubmitModel submitModel)
        {
            var ajaxResponse = _usersHelper.UpdateUserMeeting(userId, meetingId, submitModel);
            return Json(ajaxResponse);
        }

        [Route("meetings/{meetingId}/delete", Name = ControllerActionRouteNames.Users.USER_MEETINGS_DELETE)]
        public ActionResult UserMeetingsDelete(int? meetingId)
        {
            _usersHelper.DeleteUserMeeting(meetingId);
            return RedirectToRoute(ControllerActionRouteNames.Users.USER_MEETINGS);
        }

        [HttpPost]
        [Route("meetings/export-to-excel", Name = ControllerActionRouteNames.Users.USER_MEETINGS_EXPORT_TO_EXCEL)]
        public ActionResult MeetingsExportToExcel(int? userId, int? meetingStatusId, DateTime? startDate, DateTime? endDate)
        {
            _usersHelper.InitMeetingsDataForExcel(this, userId, meetingStatusId, startDate, endDate);
            return RedirectToRoute(ControllerActionRouteNames.Users.USER_MEETINGS, new { userId = userId });
        }
        #endregion

    }
}