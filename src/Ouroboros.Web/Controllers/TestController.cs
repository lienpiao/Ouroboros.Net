using Ninject;
using Ouroboros.IBLL;
using Ouroboros.Mvc.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouroboros.Web.Controllers
{
    public class TestController : BaseController
    {
        [Inject]
        public ISysUserService SysUserService { get; set; }

        public TestController()
        {
            this.AddDisposableObject(SysUserService);
        }

        public ActionResult Index()
        {
            return View(SysUserService.GetList(x => true));
        }
    }
}