using Assistant.Models;
using Service.Properties;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Assistant.Reusables.Filters
{
    public class BeforePageLoad : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = (BaseController)filterContext.Controller;
            var layoutViewModel = new LayoutViewModel();
            InitMenuItems(filterContext, ref layoutViewModel, ref controller);

            controller.ViewBag.LayoutViewModel = layoutViewModel;
        }

        private void InitMenuItems(ActionExecutingContext filterContext, ref LayoutViewModel model, ref BaseController controller)
        {
            var requestedUrl = filterContext.HttpContext.Request.RawUrl;
            model.MenuItems = new List<LayoutViewModel.MenuItem>
            {
                new LayoutViewModel.MenuItem
                {
                    Caption = "Dashboard",
                    Icon = "fa fa-dashboard",
                    Url = controller.Url.RouteUrl(ControllerActionRouteNames.Home.DASHBOARD),
                    IsActive = requestedUrl == controller.Url.RouteUrl(ControllerActionRouteNames.Home.DASHBOARD)
                },

                new LayoutViewModel.MenuItem
                {
                    Caption = "Users",
                    Icon = "fa fa-users",
                    Url = controller.Url.RouteUrl(ControllerActionRouteNames.Users.USERS),
                    IsActive = requestedUrl == controller.Url.RouteUrl(ControllerActionRouteNames.Users.USERS)
                }
            };

            model.TextAbort = Resources.TextAbort;
            model.TextSuccess = Resources.TextSuccess;
        }
    }
}