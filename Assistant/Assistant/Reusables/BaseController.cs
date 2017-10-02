using Assistant.Reusables.Filters;
using System.Web.Mvc;

namespace Assistant.Reusables
{
    [BeforePageLoad(Order = 1)]
    public class BaseController : Controller
    {
        [Route("not-found", Name = ControllerActionRouteNames.Shared.NOT_FOUND)]
        public ActionResult NotFound()
        {
            return View("NotFound");
        }
    }
}