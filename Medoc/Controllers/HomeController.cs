using Medoc.App_Start;
using Medoc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medoc.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            GetUser();
            var login = HttpContext.User.Identity.Name;
            return View();
        }
    }
}