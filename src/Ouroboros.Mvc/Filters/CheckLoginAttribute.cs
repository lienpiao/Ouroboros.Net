using Ninject;
using Ouroboros.BLL;
using Ouroboros.Common;
using Ouroboros.IBLL;
using Ouroboros.Mvc.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ouroboros.Mvc.Filters
{
    /// <summary>
    /// 统一登录验证过滤器
    /// </summary>
    public class CheckLoginAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// 统一验证Session[keyManage.LoginUser]如果为null则跳转到登陆页
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //1判断是否有贴跳过登录检查的特性标签
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLogin), false))
            {
                return;
            }

            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckLogin), false))
            {
                return;
            }

            //2 判断session是否为null
            if (filterContext.HttpContext.Session[KeyManage.LoginUser] == null)
            {
                //3 查询cookie[keyManage.RememberMe]是否不为空,如果成立则模拟用户登录,再将用户实体数据存入Session中[keyManage.LoginUser]
                if (filterContext.HttpContext.Request.Cookies[KeyManage.RememberMe] != null)
                {
                    //3.1 取出cookies中存入的uid的值
                    string uid = filterContext.HttpContext.Request.Cookies[KeyManage.RememberMe].Value;
                    //3.2 找出kernal容器获取ISysUserService接口实现类的对象实例 暂无
                    ISysUserService sysUserService = new SysUserService();
                    //3.3 根据uid查出用户
                    int userId = int.Parse(uid);
                    var user = sysUserService.GetModel(x => x.Id == userId);
                    //将用户存入session
                    if (user != null)
                    {
                        filterContext.HttpContext.Session[KeyManage.LoginUser] = user;
                    }
                    else
                    {
                        ToLogin(filterContext);
                    }

                }
                else
                {
                    //4 跳转到登录页面
                    ToLogin(filterContext);
                }


            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 跳转方法
        /// </summary>
        /// <param name="filterContext"></param>
        private static void ToLogin(ActionExecutingContext filterContext)
        {
            //1.判断当前请求是否ajax请求
            bool isAjaxRequst = filterContext.HttpContext.Request.IsAjaxRequest();
            if (isAjaxRequst)//ajax
            {
                JsonResult json = new JsonResult();
                json.Data = new { status = EnumManage.AjaxState.nologin, mas = "您未登录或者登陆已经失效,请重新登陆" };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
            }
            else//浏览器的请求
            {
                ViewResult view = new ViewResult();
                view.ViewName = "/Areas/System/Views/Shared/NoLogin.cshtml";
                filterContext.Result = view;
            }

        }
    }
}
