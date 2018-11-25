using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("菜单")]
        Menu = 10101,

        [Description("功能")]
        Operation = 10102,

        [Description("页面元素")]
        PageElement = 10103,

        [Description("文件")]
        File = 10104
    }
}
