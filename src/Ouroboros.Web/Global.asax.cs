using Ouroboros.Mvc.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ouroboros.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //利用autofac实现MVC项目的IOC和DI
            AutofacConfig.Register();
            //注册区域路由规则
            AreaRegistration.RegisterAllAreas();
            //注册网址路由
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册全局过滤器
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
