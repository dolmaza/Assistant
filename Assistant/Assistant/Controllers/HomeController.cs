using Assistant.Models;
using Assistant.Reusables;
using Service;
using System;
using System.Web.Mvc;

namespace Assistant.Controllers
{
    public class HomeController : BaseController
    {
        [Route("", Name = ControllerActionRouteNames.Home.DASHBOARD)]
        public ActionResult Dashboard()
        {
            var client = new UserService();
            string message = null;
            try
            {

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return View(new DashboardViewModel());
        }
    }
}