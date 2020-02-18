using Ouroboros.IBLL;
using Ouroboros.Mvc.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ouroboros.Web.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : BaseController
    {

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