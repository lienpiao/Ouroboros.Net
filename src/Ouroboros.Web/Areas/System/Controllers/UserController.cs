using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouroboros.Web.Areas.System.Controllers
{
    public class UserController : Controller
    {
        // GET: System/User
        public ActionResult Index()
        {
            return View();
        }
    }
}