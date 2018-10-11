using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Common
{
    /// <summary>
    /// key管理类
    /// </summary>
    public class KeyManage
    {
        /// <summary>
        /// 用于存放验证码的session  key
        /// </summary>
        public const string VCode = "ouroborosvcore";

        /// <summary>
        /// 用于存放登录成功的用户实体的session  key
        /// </summary>
        public const string LoginUser = "ouroborosloginuser";

        /// <summary>
        /// 用于存放登录成功以后的用户id的cookie key        
        /// </summary>
        public const string RememberMe = "ouroborosrememberme";

        /// <summary>
        /// 用于缓存整个autofac的容器对象的 缓存key
        /// </summary>
        public const string AutofacContainer = "ouroborosautofacContainer";

    }
}
