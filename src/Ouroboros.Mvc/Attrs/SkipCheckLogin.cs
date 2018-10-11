using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.Mvc.Attrs
{
    /// <summary>
    /// 定义自定义过滤器用于跳过登录检查，
    /// 特点：此过滤器只能贴到方法或者类上
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class SkipCheckLogin : Attribute
    {

    }
}
