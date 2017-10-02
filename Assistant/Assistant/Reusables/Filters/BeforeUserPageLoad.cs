using Assistant.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Assistant.Reusables.Filters
{
    public class BeforeUserPageLoad : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var model = new UserLayoutViewModel();
            var controller = (BaseController)filterContext.Controller;
            InitTabs(filterContext, ref model, ref controller);

            controller.ViewBag.UserLayoutViewModel = model;

        }

        private void InitTabs(ActionExecutingContext filterContext, ref UserLayoutViewModel model, ref BaseController controller)
        {
            var requestedUrl = filterContext.HttpContext.Request.RawUrl;
            var updateUrl = controller.Url.RouteUrl(ControllerActionRouteNames.Users.USERS_UPDATE);
            var expensesUrl = controller.Url.RouteUrl(ControllerActionRouteNames.Users.USER_EXPENSES);
            var meetingsUrl = controller.Url.RouteUrl(ControllerActionRouteNames.Users.USER_MEETINGS);

            model.TabItems = new List<UserLayoutViewModel.TabItem>
            {
                new UserLayoutViewModel.TabItem
                {
                    Caption = "General Properties",
                    Url = updateUrl,
                    IsActive = requestedUrl == updateUrl
                },

                new UserLayoutViewModel.TabItem
                {
                    Caption = "User Expenses",
                    Url = expensesUrl,
                    IsActive = requestedUrl == expensesUrl
                },
                new UserLayoutViewModel.TabItem
                {
                    Caption = "User Meetings",
                    Url = meetingsUrl,
                    IsActive = requestedUrl == meetingsUrl
                }
            };
        }
    }
}