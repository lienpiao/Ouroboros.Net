using Ouroboros.Common;
using Ouroboros.IBLL;
using Ouroboros.Mvc.Attrs;
using Ouroboros.Mvc.Infrastucture;
using Ouroboros.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Ouroboros.Web.Areas.System.Controllers
{
    [SkipCheckLogin]
    public class AccountController : BaseController
    {
        public AccountController(ISysUserService sysUserService)
        {
            base.SysUserService = sysUserService;
            this.AddDisposableObject(SysUserService);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel info = new LoginViewModel()
            {
                RememberMe = false
            };

            //1.0 判断cookie[Keys.IsMember]!=null 则应该将登录视图上的记住3天勾选上
            if (Request.Cookies[KeyManage.RememberMe] != null)
            {
                info.RememberMe = true;
            }

            return View(info);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                //1.实体参数合法性验证
                if (ModelState.IsValid == false)
                {
                    return WriteError("实体验证失败");
                }
                //2.检查验证码的合法性
                string vcodeFromSession = string.Empty;
                if (Session[KeyManage.VCode] != null)
                {
                    vcodeFromSession = Session[KeyManage.VCode].ToString();
                }
                if (model.VCode.IsEmpty()
                    || vcodeFromSession.Equals(model.VCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return WriteError("验证码不合法");
                }
                //3.检查用户名和密码的正确性
                string md5PWD = EncryptionHelper.Get32MD5One(model.Password);
                var userInfo = SysUserService.GetModel(x => x.UserName == model.Name && x.Password == md5PWD);
                if (userInfo == null)
                {
                    return WriteError("用户名或者密码错误");
                }
                //4.将userInfo存入session
                Session[KeyManage.LoginUser] = userInfo;
                //string userLoginId = Guid.NewGuid().ToString();
                //Common.Cache.CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));
                // Response.Cookies["userLoginId"].Value = userLoginId;
                //5.判断logininfo实体model中的RememberMe是否为true，如果成立则将用户id写入cookie中,输出给浏览器存入硬盘中，过期时间为3天
                if (model.RememberMe)
                {
                    //一般要将用户ID利用DES(对称加密算法使用自己定义的一个密码)进行加密成，将来可以使用同一个密码进行解密
                    HttpCookie cookie = new HttpCookie(KeyManage.RememberMe, userInfo.Id.ToString());
                    cookie.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    //清除cookie操作
                    HttpCookie cookie = new HttpCookie(KeyManage.RememberMe, "");
                    cookie.Expires = DateTime.Now.AddYears(-3);
                    Response.Cookies.Add(cookie);
                }
                //6.将当前用户的所有权限缓存起来，选择此缓存永久有效，当管理员操作用户分配角色和设置此用户所在角色的权限菜单的时候，要使缓存失效

                //7.返回登录成功消息
                return WriteSuccess("登录成功");
            }
            catch (Exception ex)
            {
                return WriteError(ex);
            }

        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            //1.0 清空Session[KeyManage.LoginUser]对象
            Session[KeyManage.LoginUser] = null;

            //2.0 清除cookie
            HttpCookie cookie = new HttpCookie(KeyManage.RememberMe, "");
            cookie.Expires = DateTime.Now.AddYears(-3);
            Response.Cookies.Add(cookie);

            //3.0 跳转到登录页面
            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowVCode()
        {
            //1.产生一个验证码的字符串
            ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);
            //将验证码存入session中
            Session[KeyManage.VCode] = strCode;
            //将验证码画到图片上
            byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);
            return File(imgBytes, @"imge/jpeg");
        }
    }
}