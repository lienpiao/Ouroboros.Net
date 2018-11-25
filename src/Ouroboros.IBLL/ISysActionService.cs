using Ouroboros.Common;
using Ouroboros.Model;
using System.Collections.Generic;

namespace Ouroboros.IBLL
{
    /// <summary>
    /// SysAction业务逻辑接口
    /// </summary>
    public interface ISysActionService : IBaseService<SysAction>
    {
        IList<zTree> GetMenuTree();
    }
}
