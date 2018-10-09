using Ouroboros.IBLL;
using Ouroboros.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ouroboros.BLL
{
    public class SysUserActionService : BaseService<SysUserAction>, ISysUserActionService
    {
        public SysUserActionService():base(){ }

        /// <summary>
        /// 确定当前的CurrentDao值
        /// </summary>
        /// <returns></returns>
        public override bool SetCurrentDao()
        {
            this.CurrentDao = DbSessionContext.SysUserActionDao;
            this.AddDisposableObject(this.CurrentDao);
            return true;
        }
    }
}
