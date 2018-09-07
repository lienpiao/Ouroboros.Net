using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Model.EnumList
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 10101,

        /// <summary>
        /// 功能
        /// </summary>
        Operation = 10102,

        /// <summary>
        /// 页面元素
        /// </summary>
        PageElement = 10103,

        /// <summary>
        /// 文件
        /// </summary>
        File = 10104
    }
}
