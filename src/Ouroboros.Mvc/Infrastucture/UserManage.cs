using Ouroboros.Common;
using Ouroboros.Model;
using System.Web;

namespace Ouroboros.Mvc.Infrastucture
{
    /// <summary>
    /// 负责管理用户的相关操作的
    /// </summary>
    public class UserManage
    {
        /// <summary>
        /// 负责获取当前登录用户的实体对象
        /// </summary>
        /// <returns></returns>
        public static SysUser GetCurrentUserInfo()
        {
            if (HttpContext.Current.Session[KeyManage.LoginUser] != null)
            {
                return HttpContext.Current.Session[KeyManage.LoginUser] as SysUser;
            }
            return new SysUser() { };
        }
    }
}
