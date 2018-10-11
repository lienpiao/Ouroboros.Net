using Autofac;
using Autofac.Integration.Mvc;
using Ouroboros.Common;
using Ouroboros.Common.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Ouroboros.Web
{
    public class AutofacConfig
    {
        /// <summary>
        /// 负责调用autofac框架实现业务逻辑层和数据仓储层程序集中的类型对象的创建
        /// 负责创建MVC控制器类的对象(调用控制器中的有参构造函数),接管DefaultControllerFactory的工作
        /// </summary>
        public static void Register()
        {
            //1 实例化一个autofac的创建容器
            var builder = new ContainerBuilder();

            //2 告诉Autofac框架，将来要创建的控制器类存放在哪个程序集 (itcast.CRM15.Site)
            Assembly controllerAss = Assembly.Load("Ouroboros.Web");
            builder.RegisterControllers(controllerAss);

            //3 告诉autofac框架注册数据仓储层所在程序集中的所有类的对象实例
            Assembly respAss = Assembly.Load("Ouroboros.DAL");
            //创建respAss中的所有类的instance以此类的实现接口存储
            builder.RegisterTypes(respAss.GetTypes()).AsImplementedInterfaces();//.InstancePerHttpRequest();

            //4 告诉autofac框架注册业务逻辑层所在程序集中的所有类的对象实例
            Assembly serAss = Assembly.Load("Ouroboros.BLL");
            //创建serAss中的所有类的instance以此类的实现接口存储
            builder.RegisterTypes(serAss.GetTypes()).AsImplementedInterfaces();

            //5 创建一个Autofac的容器
            var container = builder.Build();

            //6 将container对象缓存到HttpRuntime.cache中，并且是永久有效
            CacheHelper.AddCache(KeyManage.AutofacContainer, container);

            //Resolve方法可以从autofac容器中获取指定的ISysUserService的具体实现类的实体对象
            //container.Resolve<IsysuserInfoSercies>();

            //7.0 将MVC的控制器对象实例 交由autofac来创建
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}