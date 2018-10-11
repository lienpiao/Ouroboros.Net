using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Common
{
    /// <summary>
    /// 枚举管理类
    /// </summary>
    public class EnumManage
    {
        /// <summary>
        /// 负责标记ajax请求以后的json数据状态
        /// </summary>
        public enum AjaxState
        {
            /// <summary>
            /// 成功
            /// </summary>
            sucess = 0,
            /// <summary>
            /// 错误异常
            /// </summary>
            error = 1,
            /// <summary>
            /// 未登录
            /// </summary>
            nologin = 2
        }
    }
}
