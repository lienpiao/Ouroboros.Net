using Ninject;
using Ouroboros.BLL;
using Ouroboros.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ouroboros.Mvc.Infrastucture
{
    /// <summary>
    /// 设置 DI 容器:自定义Ninject控制器工厂,继承 DefaultControllerFactory（默认的控制器工厂）
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<ISysUserService>().To<SysUserService>();
            ninjectKernel.Bind<ISysRoleService>().To<SysRoleService>();
            ninjectKernel.Bind<ISysActionService>().To<SysActionService>();
            ninjectKernel.Bind<ISysUserActionService>().To<SysUserActionService>();
        }
    }
}
